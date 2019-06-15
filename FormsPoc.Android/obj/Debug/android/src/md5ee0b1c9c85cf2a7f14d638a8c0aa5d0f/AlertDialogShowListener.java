package md5ee0b1c9c85cf2a7f14d638a8c0aa5d0f;


public class AlertDialogShowListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.content.DialogInterface.OnShowListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onShow:(Landroid/content/DialogInterface;)V:GetOnShow_Landroid_content_DialogInterface_Handler:Android.Content.IDialogInterfaceOnShowListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Syncfusion.SfImageEditor.Android.AlertDialogShowListener, Syncfusion.SfImageEditor.XForms.Android", AlertDialogShowListener.class, __md_methods);
	}


	public AlertDialogShowListener ()
	{
		super ();
		if (getClass () == AlertDialogShowListener.class)
			mono.android.TypeManager.Activate ("Syncfusion.SfImageEditor.Android.AlertDialogShowListener, Syncfusion.SfImageEditor.XForms.Android", "", this, new java.lang.Object[] {  });
	}

	public AlertDialogShowListener (android.app.AlertDialog p0)
	{
		super ();
		if (getClass () == AlertDialogShowListener.class)
			mono.android.TypeManager.Activate ("Syncfusion.SfImageEditor.Android.AlertDialogShowListener, Syncfusion.SfImageEditor.XForms.Android", "Android.App.AlertDialog, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onShow (android.content.DialogInterface p0)
	{
		n_onShow (p0);
	}

	private native void n_onShow (android.content.DialogInterface p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
