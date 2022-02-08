namespace Jyrapp_position;

public static class Coordinate_conv
{
    public static Coordinate_Conv_result to_UTM(GPS_DD_Point gps)
    {
        // 2013-04-29/JeH, loukko (at) loukko (dot) net
        // http://www.loukko.net/koord_proj/
        // Vapaasti käytettävissä ilman toimintatakuuta.
        
        // Vakiot
        double f = 1 / 298.257222101; // Ellipsoidin litistyssuhde
        double a = 6378137; // Isoakselin puolikas
        double lambda_nolla = 0.471238898; // Keskimeridiaani (rad), 27 astetta
        double k_nolla = 0.9996; // Mittakaavakerroin
        double E_nolla = 500000; // Itäkoordinaatti

        // Kaavat

        // Muunnetaan astemuotoisesta radiaaneiksi
        double fii = (gps.LeveysasteDD * Math.PI) / 180;
        double lambda = (gps.PituusasteDD * Math.PI) / 180;

        double n = f / (2.0 - f);
        double A1 = (a / (1.0 + n)) * (1.0 + (Math.Pow(n, 2) / 4) + (Math.Pow(n, 4) / 64.0));
        double e_toiseen = (2 * f) - Math.Pow(f, 2);
        double e_pilkku_toiseen = e_toiseen / (1.0 - e_toiseen);
        double h1_pilkku = (1.0 / 2.0) * n - (2.0 / 3.0) * Math.Pow(n, 2) + (5.0 / 16.0) * Math.Pow(n, 3) +
                           (41.0 / 180.0) * Math.Pow(n, 4);
        double h2_pilkku = (13.0 / 48.0) * Math.Pow(n, 2) - (3.0 / 5.0) * Math.Pow(n, 3) +
                           (557.0 / 1440.0) * Math.Pow(n, 4);
        double h3_pilkku = (61.0 / 240.0) * Math.Pow(n, 3) - (103.0 / 140.0) * Math.Pow(n, 4);
        double h4_pilkku = (49561.0 / 161280.0) * Math.Pow(n, 4);
        double Q_pilkku = Math.Asinh(Math.Tan(fii));
        double Q_2pilkku = Math.Atanh(Math.Sqrt(e_toiseen) * Math.Sin(fii));
        double Q = Q_pilkku - Math.Sqrt(e_toiseen) * Q_2pilkku;
        double l = lambda - lambda_nolla;
        double beeta = Math.Atan(Math.Sinh(Q));
        double eeta_pilkku = Math.Atanh(Math.Cos(beeta) * Math.Sin(l));
        double zeeta_pilkku = Math.Asin(Math.Sin(beeta) / (1.0 / Math.Cosh(eeta_pilkku)));
        double zeeta1 = h1_pilkku * Math.Sin(2 * zeeta_pilkku) * Math.Cosh(2 * eeta_pilkku);
        double zeeta2 = h2_pilkku * Math.Sin(4 * zeeta_pilkku) * Math.Cosh(4 * eeta_pilkku);
        double zeeta3 = h3_pilkku * Math.Sin(6 * zeeta_pilkku) * Math.Cosh(6 * eeta_pilkku);
        double zeeta4 = h4_pilkku * Math.Sin(8 * zeeta_pilkku) * Math.Cosh(8 * eeta_pilkku);
        double eeta1 = h1_pilkku * Math.Cos(2 * zeeta_pilkku) * Math.Sinh(2 * eeta_pilkku);
        double eeta2 = h2_pilkku * Math.Cos(4 * zeeta_pilkku) * Math.Sinh(4 * eeta_pilkku);
        double eeta3 = h3_pilkku * Math.Cos(6 * zeeta_pilkku) * Math.Sinh(6 * eeta_pilkku);
        double eeta4 = h4_pilkku * Math.Cos(8 * zeeta_pilkku) * Math.Sinh(8 * eeta_pilkku);
        double zeeta = zeeta_pilkku + zeeta1 + zeeta2 + zeeta3 + zeeta4;
        double eeta = eeta_pilkku + eeta1 + eeta2 + eeta3 + eeta4;

        // Tulos tasokoordinaatteina

        return new Coordinate_Conv_result(A1 * zeeta * k_nolla, A1 * eeta * k_nolla + E_nolla);
    }
    
        public static Coordinate_Conv_result to_GK(GPS_DD_Point gps)
    {
        // 2013-04-29/JeH, loukko (at) loukko (dot) net
        // http://www.loukko.net/koord_proj/
        // Vapaasti käytettävissä ilman toimintatakuuta.
        
        // Vakiot
        double f = 1 / 298.257222101; // Ellipsoidin litistyssuhde
        double a = 6378137; // Isoakselin puolikas
        double lambda_nolla = (Math.PI / 180) * gps.GK;
        int k_nolla = 1; // Mittakaavakerroin
        double E_nolla = (1000000 * gps.GK) + 500000;; // Itäkoordinaatti

        // Kaavat

        // Muunnetaan astemuotoisesta radiaaneiksi
        double fii = (gps.LeveysasteDD * Math.PI) / 180;
        double lambda = (gps.PituusasteDD * Math.PI) / 180;

        double n = f / (2.0 - f);
        double A1 = (a / (1.0 + n)) * (1.0 + (Math.Pow(n, 2) / 4) + (Math.Pow(n, 4) / 64.0));
        double e_toiseen = (2 * f) - Math.Pow(f, 2);
        double e_pilkku_toiseen = e_toiseen / (1.0 - e_toiseen);
        double h1_pilkku = (1.0 / 2.0) * n - (2.0 / 3.0) * Math.Pow(n, 2) + (5.0 / 16.0) * Math.Pow(n, 3) +
                           (41.0 / 180.0) * Math.Pow(n, 4);
        double h2_pilkku = (13.0 / 48.0) * Math.Pow(n, 2) - (3.0 / 5.0) * Math.Pow(n, 3) +
                           (557.0 / 1440.0) * Math.Pow(n, 4);
        double h3_pilkku = (61.0 / 240.0) * Math.Pow(n, 3) - (103.0 / 140.0) * Math.Pow(n, 4);
        double h4_pilkku = (49561.0 / 161280.0) * Math.Pow(n, 4);
        double Q_pilkku = Math.Asinh(Math.Tan(fii));
        double Q_2pilkku = Math.Atanh(Math.Sqrt(e_toiseen) * Math.Sin(fii));
        double Q = Q_pilkku - Math.Sqrt(e_toiseen) * Q_2pilkku;
        double l = lambda - lambda_nolla;
        double beeta = Math.Atan(Math.Sinh(Q));
        double eeta_pilkku = Math.Atanh(Math.Cos(beeta) * Math.Sin(l));
        double zeeta_pilkku = Math.Asin(Math.Sin(beeta) / (1.0 / Math.Cosh(eeta_pilkku)));
        double zeeta1 = h1_pilkku * Math.Sin(2 * zeeta_pilkku) * Math.Cosh(2 * eeta_pilkku);
        double zeeta2 = h2_pilkku * Math.Sin(4 * zeeta_pilkku) * Math.Cosh(4 * eeta_pilkku);
        double zeeta3 = h3_pilkku * Math.Sin(6 * zeeta_pilkku) * Math.Cosh(6 * eeta_pilkku);
        double zeeta4 = h4_pilkku * Math.Sin(8 * zeeta_pilkku) * Math.Cosh(8 * eeta_pilkku);
        double eeta1 = h1_pilkku * Math.Cos(2 * zeeta_pilkku) * Math.Sinh(2 * eeta_pilkku);
        double eeta2 = h2_pilkku * Math.Cos(4 * zeeta_pilkku) * Math.Sinh(4 * eeta_pilkku);
        double eeta3 = h3_pilkku * Math.Cos(6 * zeeta_pilkku) * Math.Sinh(6 * eeta_pilkku);
        double eeta4 = h4_pilkku * Math.Cos(8 * zeeta_pilkku) * Math.Sinh(8 * eeta_pilkku);
        double zeeta = zeeta_pilkku + zeeta1 + zeeta2 + zeeta3 + zeeta4;
        double eeta = eeta_pilkku + eeta1 + eeta2 + eeta3 + eeta4;

        // Tulos tasokoordinaatteina

        return new Coordinate_Conv_result(A1 * zeeta * k_nolla, A1 * eeta * k_nolla + E_nolla);
    }
}