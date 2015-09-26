﻿using System.Collections;
using System.Collections.Generic;

public class BitMap8 : List<byte> { };

public class MailmanC
{
    private static MailmanC mailman = null;
    private List<SyncableObject> objects; ///< All syncable objects in game
    private List<SyncableObject> objectsToUpdate; ///< Objects that have changed
    private BitMap8 objectsBitMask; ///< Updated data bits for each object

    public void Dispatch()
    {
        //


        // Clear masks after sending
        foreach (SyncableObject s in objectsToUpdate)
        {
            objectsBitMask[s.getIndex()] = 0;
        }
    }

    public static MailmanC Instance()
    {
        if (mailman == null) {
            mailman = new MailmanC();
        }

        return mailman;
    }

    public int UnitCreated(UnitSyncC s)
    {
        objects.Add(s);
        return objects.Count;
    }

    public void ObjectUpdated(SyncableObject obj, int index, int mask)
    {
        // Add to list if not inserted yet
        if (objectsBitMask[index] == 0)
        {
            objectsToUpdate.Add(obj);
        }

        // Update the bitmask
        objectsBitMask[index] |= (byte)mask;
    }

}
