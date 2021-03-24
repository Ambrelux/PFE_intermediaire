using Res.Scripts.UserInterface;
using UnityEngine;

namespace Res.Scripts.Object
{
    public class ClickToModify : MonoBehaviour {
        void Update()
        {
            if (!UiObject.Instance.uiObjectState && !UiWalls.Instance.uiWallsState)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 100.0f))
                    {
                        if (hit.collider.gameObject.CompareTag("Furniture"))
                        {
                            if (UiObject.Instance.uiObjectState == false)
                            {
                                UiObject.Instance.ChangeState();
                                UiObject.Instance.objData = hit.collider.gameObject.GetComponent<ObjectData>();
                            }
                        }
                        else if (hit.collider.gameObject.CompareTag("Material"))
                        {
                            UiWalls.Instance.ChangeState();
                            UiWalls.Instance.objData = hit.collider.gameObject.GetComponent<ObjectData>();
                        }
                    }
                }
            }
        }
        //Contient la voiture séléctionnée actuelement. C'est un variable static, elle existe donc dans la classe et non dans instance d'objet de la classe.
        //Cela signifie que cette variable est partagée entre tous les carControllers.
        // public static CarController currentlySelectedCar;
        //
        // public void SelectCar(CarController car)
        // {
        //     //Si une voiture était séléctionnée, désactive son carController
        //     if (currentlySelectedCar != null)
        //         currentlySelectedCar.enabled = false;
        //
        //     //Séléctionne la nouvelle voiture
        //     currentlySelectedCar = car;
        //     //Active le carController pour rendre la voiture mobile
        //     currentlySelectedCar.enabled = true;
        //     //Envoi un message pour appeler une eventuelle fonction Flash qui serait sur un composant de notre gameObject.
        //     currentlySelectedCar.SendMessage("Flash", SendMessageOptions.DontRequireReceiver);
        //
        // }

    }
}