// See https://aka.ms/new-console-template for more information

using System.Globalization;
using Jyrapp_position;

var culture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

GPS_DD_Point gps = new GPS_DD_Point(63.07003952, 21.65800692, 21,36.157);

Coordinate_Conv_result utm = Coordinate_conv.to_UTM(gps);
Console.WriteLine("Leveysaste: " + gps.LeveysasteDD);
Console.WriteLine("Pituusaste: " + gps.PituusasteDD);
Console.WriteLine("N_utm: " + utm.N);
Console.WriteLine("E_utm: " + utm.E);
Coordinate_Conv_result gk = Coordinate_conv.to_GK(gps);
Console.WriteLine("N_GK21: " + gk.N);
Console.WriteLine("E_GK21: " + gk.E);
Create_geoid_dem.create_dem();
Geoid_Height geoidikorkeus = Create_geoid_dem.get_dem(gps);
Console.WriteLine("Geoidikorkeus: " + geoidikorkeus.Z);
Console.WriteLine("Korkeus N2000 järjestelmässä: "+(gps.EllipsoidHeight-geoidikorkeus.Z));

ParseFaces.To_list("koti.mm.xml");
//Kaatuu, jos piste on mallin ulkopuolella->tee virheilmoitus.
Console.WriteLine("Mallin korkeus annetussa pisteessä: "+ParseFaces.calc_point_z(gk.N, gk.E, ParseFaces.where_is_point(gk.N, gk.E)));
Console.WriteLine("Pisteen korkeusero malliin: "+(gps.EllipsoidHeight-geoidikorkeus.Z-ParseFaces.calc_point_z(gk.N,gk.E,ParseFaces.where_is_point(gk.N,gk.E))));




