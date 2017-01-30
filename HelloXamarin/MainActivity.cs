using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using HelloXamarin.Resources;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Content.PM;
using Android.Locations;

namespace HelloXamarin
{

    [Activity(Label = "HelloXamarin", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnMapReadyCallback
    {
        TextView txtTitle;
        Button btnPrimary;
        EditText titulo;
        int count = 0;
        Marker marcador;
        GoogleMap mMap;
        double lat = 0.0;
        double lng = 0.0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

          
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            MapFragment mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);

            titulo = FindViewById<EditText>(Resource.Id.titulo);
            txtTitle = FindViewById<TextView>(Resource.Id.txtTitle);
            btnPrimary = FindViewById<Button>(Resource.Id.btnPrimary);
            btnPrimary.Click += delegate {
                txtTitle.Text = titulo.Text;
                var activityTwo = new Intent(this, typeof(ActivityTwo));
                activityTwo.PutExtra("MyData", titulo.Text);
                StartActivity(activityTwo);
            };

        }

        public void OnMapReady(GoogleMap googleMap)
        {
            mMap = googleMap;
            MarkerOptions markerOptions = new MarkerOptions();
            LatLng latLng = new LatLng(10.9942032, -74.7915192);
            markerOptions.SetPosition(latLng);
            markerOptions.SetTitle("Universidad Simon Bolivar");
            mMap.AddMarker(markerOptions);

            //googleMap.UiSettings.ZoomControlsEnabled = true;
            //googleMap.UiSettings.CompassEnabled = true;
            //googleMap.AnimateCamera(CameraUpdateFactory.ZoomTo(150));
            mMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(latLng, 16));

            //throw new NotImplementedException();
        }


        /*/----mover el marcador
        private void agregarMarcador(double lat, double lng)
        {
            LatLng coordenadas = new LatLng(lat, lng);
            CameraUpdate miUbicacion = CameraUpdateFactory.NewLatLngZoom(coordenadas, 16);
            if (marcador != null) marcador.Remove();
            marcador = mMap.AddMarker(new MarkerOptions()
                    .SetPosition(coordenadas)
                    .SetTitle("Ubicación Actual"));
            mMap.AnimateCamera(miUbicacion);
            txtTitle.Text=lat + "," + lng;
        }

        private void actualizarubicacion(Android.Locations.Location location)
        {
            if (location != null)
            {
                lat = location.Latitude;
                lng = location.Longitude;
                agregarMarcador(lat, lng);
            }
        }

        private void miUbicacion()
        {

            if (Android.Support.V4.App.ActivityCompat.CheckSelfPermission(this, Android.Manifest.Permission.AccessFineLocation) != Permission.Granted && Android.Support.V4.App.ActivityCompat.CheckSelfPermission(this, Android.Manifest.Permission.AccessCoarseLocation) != Permission.Granted)
            {

                
                return;
            }
            Android.Locations.LocationManager locationManager = (Android.Locations.LocationManager)GetSystemService(Context.LocationService);
            Android.Locations.Location location = locationManager.GetLastKnownLocation(Android.Locations.LocationManager.GpsProvider);
            actualizarubicacion(location);
            locationManager.RequestLocationUpdates(Android.Locations.LocationManager.GpsProvider, 15000, 0, (Android.Locations.Location), LocListener);
        }



        
        
        //----mover el marcador */


    }
}

