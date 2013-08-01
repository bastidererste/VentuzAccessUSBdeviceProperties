using System;
using Ventuz.Kernel;
using System.Collections.Generic;
using System.Management; //add System.Management to your project references from GAC.



public class Script : ScriptBase, System.IDisposable
{
    
    // This member is used by the Validate() method to indicate
    // whether the Generate() method should return true or false
    // during its next execution.
    private bool changed;
    // create temp array
    string[] _deviceID;

    // This Method is called if the component is loaded/created.
    public Script()
    {
    		//create the search query to select all "*" devices "within" Win32_USBHub
		ManagementObjectSearcher search = new ManagementObjectSearcher ("Select * From Win32_USBHub");
		
		//create a temp array the size of the search result 
		_deviceID = new string[search.Get().Count];

		//create an indexer
		int i = 0;
		
		//get each USBdevice in the search result
		foreach (ManagementBaseObject mbo in search.Get())
		{
		//assign the deviceID of each USBDevice in the search result to the temp array at position i
		_deviceID[i] = mbo["DeviceID"].ToString();
	 
		//increment indexer
		i++;
		}
		
		changed = true;
		
        // Note: Accessing input or output properties from this method
        // will have no effect as they have not been allocated yet.
    }
    
    // This Method is called if the component is unloaded/disposed
    public virtual void Dispose()
    {
    }
    
    // This Method is called if an input property has changed its value
    public override void Validate()
    {
        // Remember: set changed to true if any of the output 
        // properties has been changed, see Generate()
    }
    
    // This Method is called every time before a frame is rendered.
    // Return value: if true, Ventuz will notify all nodes bound to this
    //               script node that one of the script's outputs has a
    //               new value and they therefore need to validate. For
    //               performance reasons, only return true if output
    //               values really have been changed.
    public override bool Generate()
    {
    		// assaign temp array to the output array.
		// See here why temporary arrays are needed: http://sebastianspiegl.de/?q=Working-with-matrices-and-arrays-in-Ventuz-C%23-scripts%20		
		deviceIDs = _deviceID;
		
        if (changed)
        {
            changed = false;
            return true;
        }

        return false;
    }
}
