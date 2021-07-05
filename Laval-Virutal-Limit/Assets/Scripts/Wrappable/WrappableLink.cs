using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappableLink
{
    private Wrappable _wrappable;

    public Wrappable Wrappable => _wrappable;

    private ConveyorPad _pad;

    public ConveyorPad Pad => _pad;


    public static void Connect(Wrappable wrappable, ConveyorPad pad)
    {
        if (pad.Link != null)
        {
            return;
        }

        wrappable.Link?.Disconnect();

        WrappableLink link = new WrappableLink();
        link._pad = pad;
        link._wrappable = wrappable;

        wrappable.Link = link;
        pad.Link = link;
    }

    public void Disconnect()
    {
        _pad.Link = null;
        _wrappable.Link = null;
    }

}
