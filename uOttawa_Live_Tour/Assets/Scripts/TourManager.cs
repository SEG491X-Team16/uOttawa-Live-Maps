using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourManager : MonoBehaviour
{

    public PathManager pathManager;

    public POIManager poiManager;

    public BuildingHighlight buildingHighlighter;

    public MoveMarker destinationMarker;

    public SceneSwitch sceneSwitch;

    private Path _path;

    // Start is called before the first frame update
    void Start()
    {
        //prevent the screen from going to sleep
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        this._path = getTabaretPath();
        pathManager.SetCurrentPath(this._path);

        buildingHighlighter.SetBuildingHighlight(this._path.GetCurrentPOI().BuildingHighlight);
        destinationMarker.lat = this._path.GetCurrentPOI().Coordinates.Latitude;
        destinationMarker.lon = this._path.GetCurrentPOI().Coordinates.Longitude;

        // poiManager.AddPOI(this._path.GetCurrentPOI());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPathSegmentFinished() {
        Debug.Log("path segment finished callback");
        poiManager.AddPOI(this._path.GetCurrentPOI());
    }

    public void OnPOIAchieved() {
        Debug.Log("POI achieved callback");
        pathManager.CleanupCurrentSegment();
    }

    public void OnContinueTour() {
        Debug.Log("continue tour callback");
        poiManager.RemovePOI(this._path.GetCurrentPOI());
        buildingHighlighter.ClearBuildingHighlight(this._path.GetCurrentPOI().BuildingHighlight);

        //if we've reached the end, go back to the main menu
        if (!pathManager.StartNextPathSegment()) {
            sceneSwitch.loadMenu();
        }

        buildingHighlighter.SetBuildingHighlight(this._path.GetCurrentPOI().BuildingHighlight);
        destinationMarker.lat = this._path.GetCurrentPOI().Coordinates.Latitude;
        destinationMarker.lon = this._path.GetCurrentPOI().Coordinates.Longitude;
    }

    private Path getPath() {
        //TODO: get the path from the database

        Path path = new Path();
        
        //sgement 1
        Waypoint way1 = new Waypoint(new GPSCoords(45.054411f, -75.640017f), 1);
        Waypoint way2 = new Waypoint(new GPSCoords(45.054515f, -75.640025f), 2);
        Waypoint way3 = new Waypoint(new GPSCoords(45.054614f, -75.640073f), 3);

        Waypoint[] ways1 = new Waypoint[] {way1, way2, way3 };
        PathSegment seg1 = new PathSegment(ways1);

        //segment 2
        Waypoint way4 = new Waypoint(new GPSCoords(45.054439f, -75.639727f), 4);
        Waypoint way5 = new Waypoint(new GPSCoords(45.054436f, -75.639698f), 5);

        Waypoint[] ways2 = new Waypoint[] {way4, way5};
        PathSegment seg2 = new PathSegment(ways2);

        PathSegment[] segments = new PathSegment[] {seg1, seg2};

        //POIs
        GPSCoords arcPos = new GPSCoords(45.420713f, -75.678542f);
        GPSCoords crxPos = new GPSCoords(45.421709f, -75.681234f);

        PointOfInterest pOI1 = new PointOfInterest("ua-3466088c3f3206288d98e66062cf15c5", "ARC", arcPos);
        PointOfInterest pOI2 = new PointOfInterest("ua-3466088c3f3206288d98e66062cf15c5", "CRX", crxPos);

        PointOfInterest[] pois = new PointOfInterest[] {pOI1, pOI2};

        path.SetSegmentsAndPOIs(segments, pois);

        Debug.Log("path created");

        return path;
    }

    private Path getTabaretPath() {
        //TODO: get the path from the database

        Path path = new Path();

        // Path Segment #1
        Waypoint way1 = new Waypoint(new GPSCoords(45.424638f, -75.685985f), 1);
        
        Waypoint[] ways1 = new Waypoint[] {way1};
        PathSegment seg1 = new PathSegment(ways1);
        
        // Path,Segment #2
        Waypoint way4 = new Waypoint(new GPSCoords(45.424620f, -75.685984f), 4);
        Waypoint way5 = new Waypoint(new GPSCoords(45.424701f, -75.686042f), 5);
        Waypoint way2 = new Waypoint(new GPSCoords(45.424810f, -75.686134f), 2);
        Waypoint way3 = new Waypoint(new GPSCoords(45.424906f, -75.686210f), 3);

        Waypoint[] ways2 = new Waypoint[] {way4, way5, way2, way3};
        PathSegment seg2 = new PathSegment(ways2);

        //path segment #3
        Waypoint way6 = new Waypoint(new GPSCoords(45.425017f, -75.686314f), 6);
        Waypoint way7 = new Waypoint(new GPSCoords(45.425115f, -75.686395f), 7);
        Waypoint way8 = new Waypoint(new GPSCoords(45.425041f, -75.686601f), 8);
        Waypoint way9 = new Waypoint(new GPSCoords(45.424991f, -75.686748f), 9);

        Waypoint[] ways3 = new Waypoint[] {way6, way7, way8, way9};
        PathSegment seg3 = new PathSegment(ways3);

        PathSegment[] segments = new PathSegment[] {seg1, seg2, seg3};

        //Points of Interest
        GPSCoords signPos = new GPSCoords(45.424552f, -75.685973f);
        GPSCoords statuePos = new GPSCoords(45.425004f, -75.686192f);
        GPSCoords haganPos = new GPSCoords(45.425038f, -75.686832f);

        PointOfInterest pOI1 = new PointOfInterest("ua-6a4d616b518ee56e51b2dd0f354abba4", "TBT", signPos);
        PointOfInterest pOI2 = new PointOfInterest("ua-8b8ebc2d7f303d5e749506701d75d6da", "TBT", statuePos);
        PointOfInterest pOI3 = new PointOfInterest("ua-3ed9aac7f115ccc7560ff188518fc26a", "HGN", haganPos);

        PointOfInterest[] pois = new PointOfInterest[] {pOI1, pOI2, pOI3};

        path.SetSegmentsAndPOIs(segments, pois);

        Debug.Log("path created");

        return path;
    }
}