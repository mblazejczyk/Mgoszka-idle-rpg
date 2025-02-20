﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;


namespace QPathFinder
{
    

    public class MouseClickDemo : MonoBehaviour
    {
        public Camera _camera;   // Needed for mouse click to world position convertion. 
        public float playerSpeed = 20.0f;
        public bool autoRotateTowardsDestination = true;
        public GameObject playerObj;


        // For PathFollowerWithGroundSnap - This will snap the player to the ground while it follows the path. 
        public float playerFloatOffset;     // This is how high the player floats above the ground. 
        public float raycastOriginOffset;   // This is how high above the player u want to raycast to ground. 
        public int raycastDistanceFromOrigin = 40;   // This is how high above the player u want to raycast to ground. 
        public bool thoroughPathFinding = false;    // uses few extra steps in pathfinding to find accurate result. 

        public bool useGroundSnap = false;          // if snap to ground is not used, player goes only through nodes and doesnt project itself on the ground. 

        public QPathFinder.Logger.Level debugLogLevel;
        public float debugDrawLineDuration;


        void Awake()
        {
               
            QPathFinder.Logger.SetLoggingLevel( debugLogLevel );
            QPathFinder.Logger.SetDebugDrawLineDuration ( debugDrawLineDuration );

        }

        public void MoveHere (GameObject place)
        {
            float distance = Vector3.Distance(playerObj.transform.position, place.transform.position);
            Debug.Log(distance);
            if (distance >= 0.35f)
            {
                PathFinder.instance.FindShortestPathOfPoints(playerObj.transform.position, place.transform.position, PathFinder.instance.graphData.lineType,
                        Execution.Asynchronously,
                        thoroughPathFinding ? SearchMode.Complex : SearchMode.Simple,
                        delegate (List<Vector3> points)
                        {
                            PathFollowerUtility.StopFollowing(playerObj.transform);
                            if (useGroundSnap)
                            {
                                FollowThePathWithGroundSnap(points);
                            }
                            else
                                FollowThePathNormally(points);

                        }
                     );
            }
        }

        
        public void FindAndMove(string objTag)
        {

            GameObject[] a;
            a = GameObject.FindGameObjectsWithTag(objTag);
            Transform[] tr = {null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            for (int i = 0; i < a.Length; i++)
            {
                tr[i] = a[i].transform;
            }


            Transform minRan = null;
            float minDist = Mathf.Infinity;
            Vector3 currPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            foreach(Transform t in tr)
            {
                if(t == null)
                {
                    break;
                }
                Debug.Log(t.position + "    " + currPos);
                float dist = Vector3.Distance(t.position, currPos);
                if(dist < minDist)
                {
                    minRan = t;
                    minDist = dist;
                }
            }


            PathFinder.instance.FindShortestPathOfPoints(playerObj.transform.position, minRan.position, PathFinder.instance.graphData.lineType,
                    Execution.Asynchronously,
                    thoroughPathFinding ? SearchMode.Complex : SearchMode.Simple,
                    delegate (List<Vector3> points)
                    {
                        PathFollowerUtility.StopFollowing(playerObj.transform);
                        if (useGroundSnap)
                        {
                            FollowThePathWithGroundSnap(points);
                        }
                        else
                            FollowThePathNormally(points);

                    }
                 );
        }

        void MovePlayerToMousePosition()
        {
			//Debug.LogError(PathFinder.instance.graphData.groundColliderLayerName + " " + LayerMask.NameToLayer( PathFinder.instance.graphData.groundColliderLayerName ));
            LayerMask backgroundLayerMask = 1 << LayerMask.NameToLayer( PathFinder.instance.graphData.groundColliderLayerName );

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 hitPos = Vector3.zero;
            if (Physics.Raycast(ray, out hit, 10000f, backgroundLayerMask))
            {
                hitPos = hit.point;
            }
            else 
            {
                Debug.LogError("ERROR!");
                return;
            }

            {
                PathFinder.instance.FindShortestPathOfPoints( playerObj.transform.position, hitPos,  PathFinder.instance.graphData.lineType, 
                    Execution.Asynchronously,
                    thoroughPathFinding ? SearchMode.Complex: SearchMode.Simple,
                    delegate ( List<Vector3> points ) 
                    { 
                        PathFollowerUtility.StopFollowing( playerObj.transform );
                        if ( useGroundSnap )
                        {
                           FollowThePathWithGroundSnap ( points );
                        }
                        else 
                            FollowThePathNormally ( points );

                    }
                 );
            }
        }

        void FollowThePathWithGroundSnap ( List<Vector3> nodes )
        {
            PathFollowerUtility.FollowPathWithGroundSnap ( playerObj.transform, 
                                                        nodes, 
                                                        playerSpeed, 
                                                        autoRotateTowardsDestination,
                                                        Vector3.down, playerFloatOffset, LayerMask.NameToLayer( PathFinder.instance.graphData.groundColliderLayerName ),
                                                        raycastOriginOffset, raycastDistanceFromOrigin );
        }

        void FollowThePathNormally ( List<Vector3> nodes )
        {
            PathFollowerUtility.FollowPath ( playerObj.transform, nodes, playerSpeed, autoRotateTowardsDestination );
        }
    }
}
