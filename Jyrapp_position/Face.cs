namespace Jyrapp_position;

public class Face
{
    public Points_Faces p1;
    public Points_Faces p2;
    public Points_Faces p3;

    public Face(Points_Faces p1, Points_Faces p2, Points_Faces p3)
    {
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
    }
}