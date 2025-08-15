namespace Jyrapp_position;

public class Create_geoid_dem_FIN2023N2000
{
        private static dem_node[,] dem = new dem_node[1140, 650];

    public static void create_dem()
    {
        int row = 1140;
        int col = 1;
        double lat;
        double lon;
        double height;
        String[] input = File.ReadAllText("FIN2023N2000.lst").Split('\n');
        //70.700000  17.480000  34.415
        //70,00000000  10,04000000  41,5820
        //lat = Double.Parse(input[i].Substring(0,11));
        //lon = Double.Parse(input[i].Substring(13,11));
        //height = Double.Parse(input[i].Substring(26));
        //60.280000  24.160000  19.184 tama on FIN2005N00.lst formaatti
        //"   67.000000   30.120000   17.566" tama on FIN2023N2000.lst formaatti

        for (int i = 0; i < 741000; i++)
        {
            lat = Double.Parse(input[i].Substring(3, 9));
            lon = Double.Parse(input[i].Substring(15, 9));
            height = Double.Parse(input[i].Substring(27));
            dem[row - 1, col - 1].lat = lat;
            dem[row - 1, col - 1].lon = lon;
            dem[row - 1, col - 1].height = height;
            col++;

            if (col > 650)
            {
                col = 1;
                row--;
            }
        }
    }

    public static Geoid_Height get_dem(GPS_DD_Point gps)
    {
        int row;
        int col;

        double x, x1, x2, y, y1, y2;
        double q11, q12, q21, q22;
        double r1, r2, p;

        row = (int) ((double) ((gps.LeveysasteDD - 58.8) / 0.01));
        col = (int) ((double) ((gps.PituusasteDD - 19.0) / 0.02));

        q12 = dem[row + 1, col].height;
        q22 = dem[row + 1, col + 1].height;
        q11 = dem[row, col].height;
        q21 = dem[row, col + 1].height;

        x = gps.PituusasteDD;
        x1 = dem[row, col].lon;
        x2 = dem[row, col + 1].lon;

        y = gps.LeveysasteDD;
        y1 = dem[row, col].lat;
        y2 = dem[row + 1, col].lat;

        r1 = ((x2 - x) / (x2 - x1)) * q11 + ((x - x1) / (x2 - x1)) * q21;
        r2 = ((x2 - x) / (x2 - x1)) * q12 + ((x - x1) / (x2 - x1)) * q22;
        p = ((y2 - y) / (y2 - y1)) * r1 + ((y - y1) / (y2 - y1)) * r2;
        
        return new Geoid_Height(p);
    }
}