namespace Jyrapp_position;
/// <summary>
/// Points_Faces pitää sisällään vain kolme kappaletta koordinaatteja.
/// </summary>
public class Points_Faces
{
    public double x;
    public double y;
    public double z;

    public Points_Faces(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public string GetPoint()
    {
        return $"x:{x}, y:{y}, z:{z}";
    }

    public double GetPointX()
    {
        return x;
    }

    public double GetPointY()
    {
        return y;
    }

    public double GetPointZ()
    {
        return z;
    }
}