using System;
using Ventuz.Kernel;
 using System.Collections.Generic;
  using System.Management; // need to add System.Management to your project references.



public class Script : ScriptBase, System.IDisposable
{
    
    // This member is used by the Validate() method to indicate
    // whether the Generate() method should return true or false
    // during its next execution.
    private bool changed;
  string[] _deviceID;

    // This Method is called if the component is loaded/created.
    public Script()
    {
		ManagementObjectSearcher Search = new ManagementObjectSearcher ("Select * From Win32_USBHub");
		
		_deviceID = new string[Search.Get().Count];

		int i = 0;
		foreach (ManagementBaseObject mbo in Search.Get())
		{
		_deviceID[i] = mbo["DeviceID"].ToString();
	 
	
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
		
		deviceIDs = _deviceID;
		
        if (changed)
        {
            changed = false;
            return true;
        }

        return false;
    }
}
