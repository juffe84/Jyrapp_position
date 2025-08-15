// See https://aka.ms/new-console-template for more information

using System.Globalization;
using Jyrapp_position;

var culture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;
//Kuusamon suunta 66.825717, 28.987300
GPS_DD_Point gps = new GPS_DD_Point(63.07003952, 21.65800692, 21,36.157); //vaasa
//GPS_DD_Point gps = new GPS_DD_Point(67, 30, 21,36.157); //kuusamon suuntaa

Coordinate_Conv_result utm = Coordinate_conv.to_UTM(gps);
Console.WriteLine("Leveysaste: " + gps.LeveysasteDD);
Console.WriteLine("Pituusaste: " + gps.PituusasteDD);
Console.WriteLine("N_utm: " + utm.N);
Console.WriteLine("E_utm: " + utm.E);
Coordinate_Conv_result gk = Coordinate_conv.to_GK(gps);
Console.WriteLine("N_GK21: " + gk.N);
Console.WriteLine("E_GK21: " + gk.E);
Create_geoid_dem.create_dem();
Create_geoid_dem_FIN2023N2000.create_dem();
Geoid_Height geoidikorkeus = Create_geoid_dem.get_dem(gps);
Geoid_Height geoidikorkeusFIN2023N2000 = Create_geoid_dem_FIN2023N2000.get_dem(gps);
Console.WriteLine("Geoidikorkeus FIN2005N00 (N2000 vanha): " + geoidikorkeus.Z);
Console.WriteLine("Geoidikorkeus FIN2023N2000 (N2000 uusi): " + geoidikorkeusFIN2023N2000.Z);
Console.WriteLine("Korkeus N2000(vanha) järjestelmässä: "+(gps.EllipsoidHeight-geoidikorkeus.Z));
Console.WriteLine("Korkeus N2000(uusi) järjestelmässä: "+(gps.EllipsoidHeight-geoidikorkeusFIN2023N2000.Z));

ParseFaces.To_list("koti.mm.xml");
//Kaatuu, jos piste on mallin ulkopuolella->tee virheilmoitus.
Console.WriteLine("Mallin korkeus annetussa pisteessä: "+ParseFaces.calc_point_z(gk.N, gk.E, ParseFaces.where_is_point(gk.N, gk.E)));
Console.WriteLine("Pisteen korkeusero malliin: "+(gps.EllipsoidHeight-geoidikorkeus.Z-ParseFaces.calc_point_z(gk.N,gk.E,ParseFaces.where_is_point(gk.N,gk.E))));




