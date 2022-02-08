using System.Globalization;
using System.Xml;

namespace Jyrapp_position;

public class ParseFaces
{
    public static List<Face> faces = new List<Face>();
    public static List<Face> To_list(string path)
    {
        
        //Tehdään dictionary pointeista läpikäymisen nopeuttamiseksi (Dictionary on kaksiulotteinen lista, joka pitää sisällään jonkun arvon, ja avaimen jolla sen voi etsiä)
        IDictionary<int, Points_Faces> points = new Dictionary<int, Points_Faces>();
        //List<Face> faces = new List<Face>();

        //avataan xml dokumentti
        XmlDocument xDoc = new XmlDocument();
        try
        {
            xDoc.Load(path);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.ReadKey();
            Environment.Exit(0);
        }

        //kerätään sekä point-nodet ja face-nodet
        XmlNodeList pNodes = xDoc.GetElementsByTagName("P");
        XmlNodeList fNodes = xDoc.GetElementsByTagName("F");

        //käydään läpi ja lisätään kaikki nodet dictionaryyn
        foreach (XmlNode node in pNodes)
        {
            string point = node.InnerText;
            string[] parts = point.Split(' ');

            int id = int.Parse(node.Attributes[0].InnerText);
            double x = double.Parse(parts[0], CultureInfo.InvariantCulture);
            double y = double.Parse(parts[1], CultureInfo.InvariantCulture);
            double z = double.Parse(parts[2], CultureInfo.InvariantCulture);

            points.Add(id, new Points_Faces(x, y, z));
        }

        //käydään läpi kaikki facet ja niiden sisältämät id:t, ja haetaan dictionarystä vastaavat Pointit
        foreach (XmlNode node in fNodes)
        {
            string face = node.InnerText;
            string[] parts = face.Split(' ');

            int id1 = int.Parse(parts[0]);
            int id2 = int.Parse(parts[1]);
            int id3 = int.Parse(parts[2]);
            Points_Faces p1 = points[id1];
            Points_Faces p2 = points[id2];
            Points_Faces p3 = points[id3];

            faces.Add(new Face(p1, p2, p3));
        }

        return faces;
    }
    
    public static int where_is_point(double cx, double cy)
    {

        double inv, u, v, v0x, v0y, v1x, v1y, v2x, v2y, dot00, dot01, dot02, dot11, dot12;
        int i = 0;
        foreach (Face face in faces)
        {
            v0x = face.p3.x - face.p1.x;
            v0y = face.p3.y - face.p1.y;
            v1x = face.p2.x - face.p1.x;
            v1y = face.p2.y - face.p1.y;
            v2x = cx - face.p1.x;
            v2y = cy - face.p1.y;
            dot00 = v0x * v0x + v0y * v0y;
            dot01 = v0x * v1x + v0y * v1y;
            dot02 = v0x * v2x + v0y * v2y;
            dot11 = v1x * v1x + v1y * v1y;
            dot12 = v1x * v2x + v1y * v2y;
            inv = 1 / (dot00 * dot11 - dot01 * dot01);
            u = (dot11 * dot02 - dot01 * dot12) * inv;
            v = (dot00 * dot12 - dot01 * dot02) * inv;

            if (u >= 0 && v >= 0 && u + v < 1)
            {
                return i;
            }
            i++;
        }

        return -1;
    }
    
    public static double calc_point_z(double x, double y, int i)
    {
        //tämä interpoloi korkeutta vaikka piste olisi kolmion ulkopuolella.

        /*
        https://stackoverflow.com/questions/13916387/finding-the-z-value-of-a-point-in-a-triangle

        */
        //double x =45.698;
        //double y=14.584;

        double x1 = faces[i].p1.x;
        double x2 = faces[i].p2.x;
        double x3 = faces[i].p3.x;
        double y1 = faces[i].p1.y;
        double y2 = faces[i].p2.y;
        double y3 = faces[i].p3.y;
        double z1 = faces[i].p1.z;
        double z2 = faces[i].p2.z;
        double z3 = faces[i].p3.z;
        double z = 0;

        double ylos = z3 * (x - x1) * (y - y2) + z1 * (x - x2) * (y - y3) + z2 * (x - x3) * (y - y1) - z2 * (x - x1) * (y - y3) - z3 * (x - x2) * (y - y1) - z1 * (x - x3) * (y - y2);

        double alas = (x - x1) * (y - y2) + (x - x2) * (y - y3) + (x - x3) * (y - y1) - (x - x1) * (y - y3) - (x - x2) * (y - y1) - (x - x3) * (y - y2);

        z = ylos / alas;
        //Console.WriteLine(z);
        return z;

    }
}

