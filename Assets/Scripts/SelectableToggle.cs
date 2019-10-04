using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SelectableToggle : MonoBehaviour
{
    public Image selectImage;

    public SelectableToggle selectionLeft;
    public SelectableToggle selectionRight;
    public SelectableToggle selectionUp;
    public SelectableToggle selectionDown;

    public UnityEvent selectionEvent;

    public SelectableToggle SelectOnLeft()
    {
        if (selectionLeft)
        {
            Deselect();
            selectionLeft.Select();
            return selectionLeft;
        }
        else return this;
    }
    public SelectableToggle SelectOnRight()
    {
        if (selectionRight)
        {
            Deselect();
            selectionRight.Select();
            return selectionRight;
        }
        else return this;
    }
    public SelectableToggle SelectOnUp()
    {
        if (selectionUp)
        {
            Deselect();
            selectionUp.Select();
            return selectionUp;
        }
        else return this;
    }
    public SelectableToggle SelectOnDown()
    {
        if (selectionDown)
        {
            Deselect();
            selectionDown.Select();
            return selectionDown;
        }
        else return this;
    }
    public void Select()
    {
        selectImage.enabled = true;
        selectionEvent.Invoke();
    }
    public void Deselect()
    {
        selectImage.enabled = false;
    }
}