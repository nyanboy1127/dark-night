using UnityEngine;
using UnityEngine.Events;

public class SelectionManager : MonoBehaviour
{
    public Objects door, lockedDoor;
    private Transform _selection;
    UnityEvent onInteract;
    UIManager uimanager;
    AnimationManager animationManager;
    Interactable interactable;

    private void Awake() {
        uimanager = FindObjectOfType<UIManager>();
        door.doorOpened = false;
        lockedDoor.doorOpened = false;
        lockedDoor.puzzleKeyPicked = false;
    }

    private void Update() {

        //klo udah gak diarahin
        if (_selection != null) {
            uimanager.objectInteraction.gameObject.SetActive(false);
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //mau ngarahin ke objek
        if (Physics.Raycast(ray, out hit, 2)) {
            var selection = hit.transform;

            if (selection.CompareTag("Selectable")){
                uimanager.ObjectInteraction();
                if (hit.collider.GetComponent<Interactable>() != false) {
                    onInteract = hit.collider.GetComponent<Interactable>().onInteract;
                    if (Input.GetKey(KeyCode.E)) {
                        onInteract.Invoke();
                    }
                }
            }

            if (selection.CompareTag("Door")) {
                
                if (door.doorOpened == false) {
                    uimanager.doorOpenInteraction();
                } else {
                    uimanager.doorCloseInteraction();
                }

                if (hit.collider.GetComponent<Interactable>() != false) {
                    onInteract = hit.collider.GetComponent<Interactable>().onInteract;
                    animationManager = hit.collider.GetComponent<AnimationManager>();
                    if (Input.GetKeyDown(KeyCode.E)) {
                        if (door.doorOpened == false) {
                            onInteract.Invoke();
                            animationManager.openDoor();
                            door.doorOpened = true;
                        } else if (door.doorOpened == true) {
                            onInteract.Invoke();
                            animationManager.closeDoor();
                            door.doorOpened = false;
                        }
                    }
                }
            }

            if (selection.CompareTag("LockedDoor")) {
                if (door.doorOpened == false) {
                    uimanager.doorOpenInteraction();
                } else {
                    uimanager.doorCloseInteraction();
                }

                if (hit.collider.GetComponent<Interactable>() != false) {
                    onInteract = hit.collider.GetComponent<Interactable>().onInteract;
                    animationManager = hit.collider.GetComponent<AnimationManager>();

                    if (Input.GetKeyDown(KeyCode.E)) {
                        if (lockedDoor.puzzleKeyPicked == true) {
                            if (lockedDoor.doorOpened == false) {
                                onInteract.Invoke();
                                animationManager.openDoor();
                                door.doorOpened = true;
                            } else if (lockedDoor.doorOpened == true) {
                                onInteract.Invoke();
                                animationManager.closeDoor();
                                door.doorOpened = false;
                            }
                        } else if(lockedDoor.puzzleKeyPicked == false) {
                            hit.collider.GetComponent<Interactable>().displayDescription();
                        }
                    }    
                }
            }

                _selection = selection;
        }
    }
    
}
