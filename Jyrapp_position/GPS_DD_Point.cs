namespace Jyrapp_position;

public class GPS_DD_Point
{
    public double LeveysasteDD, PituusasteDD, EllipsoidHeight;
    public int GK;

    public GPS_DD_Point(double _leveysasteDd, double _pituusasteDd, int _gk, double _ellipsoidHeight)
    {
        this.LeveysasteDD = _leveysasteDd;
        this.PituusasteDD = _pituusasteDd;
        this.GK = _gk;
        this.EllipsoidHeight = _ellipsoidHeight;

    }
}