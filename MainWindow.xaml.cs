using BruTile.MbTiles;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Projection;
using SQLite;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadMap();
            /*
            Mapsui.Geometries.Point point = new Mapsui.Geometries.Point(33.047564, 34.698850);
            var sphericalPoint = SphericalMercator.FromLonLat(point.X, point.Y);
            MyMapControl.Map.Home = n => n.NavigateTo(point, 12, 0, null);*/

            Mapsui.Geometries.Point center = new Mapsui.Geometries.Point(33.047564, 34.698850);
            Mapsui.Geometries.Point sphericalMercatorCoordinate = SphericalMercator.FromLonLat(center.X, center.Y);
            MyMapControl.Map.Home = n => n.NavigateTo(sphericalMercatorCoordinate, 12);

            MyMapControl.Map.Widgets.Add(new Mapsui.Widgets.Zoom.ZoomInOutWidget { MarginX = 10, MarginY = 40 });

        }

        private void LoadMap()
        {
            try
            {
                var mainDir = "../../../";                
                string path1 = mainDir + @"tiles";

                string path2 = Path.GetFullPath(Path.Combine(path1, "cymap.mbtiles"));
                var mbTilesTileSource = new MbTilesTileSource(new SQLiteConnectionString(path2, true));
                var mbTilesLayer = new TileLayer(mbTilesTileSource) { Name = "regular" };
                MyMapControl.Map.Layers.Add(mbTilesLayer);
                // MyMapControl.Map.Layers.Add(OpenStreetMap.CreateTileLayer());               
            } catch (Exception ex)
            {
                string message = ex.Message;
            }
        }      

     
    }
}
