using System.Collections;
using UnityEngine;
using Leopotam.Ecs;

sealed class InputSystem : IEcsRunSystem
{
    private Ray ray;
    private RaycastHit hit;
    private StaticData staticData;
    private EcsFilter<Tower, ObjectComponent> towerFilter;
    RaycastHit2D rayHit;

    public void Run()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 6))
            //    foreach (int i in towerFilter)
            //    {
            //        {
            //            ref Tower tower = ref towerFilter.Get1(i);
            //            ref ObjectComponent towerObjComp = ref towerFilter.Get2(i);
            //            if (hit.collider.gameObject == towerObjComp.ObGo)
            //            {
            //                ref EcsEntity towerEntity = ref towerFilter.GetEntity(i);
            //                towerEntity.Get<UpgradeTowerMarker>();
            //            }
            //        }
            //    }



            rayHit = Physics2D.GetRayIntersection(
                Camera.main.ScreenPointToRay(Input.mousePosition)
            );
            if (rayHit)
                foreach (int i in towerFilter)
                {
                    {
                        ref Tower tower = ref towerFilter.Get1(i);
                        ref ObjectComponent towerObjComp = ref towerFilter.Get2(i);
                        if (rayHit.collider.gameObject == towerObjComp.ObGo)
                        {
                            ref EcsEntity towerEntity = ref towerFilter.GetEntity(i);
                            towerEntity.Get<UpgradeTowerMarker>();
                        }
                    }
                }
        }
    }
}
