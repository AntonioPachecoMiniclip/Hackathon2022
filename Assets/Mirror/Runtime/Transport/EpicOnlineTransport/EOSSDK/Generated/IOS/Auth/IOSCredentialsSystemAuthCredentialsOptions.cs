// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Auth
{
	/// <summary>
	/// Options for initializing login for IOS.
	/// </summary>
	public class IOSCredentialsSystemAuthCredentialsOptions : ISettable
	{
		/// <summary>
		/// When calling <see cref="AuthInterface.Login" />
		/// NSObject that implements the ASWebAuthenticationPresentationContextProviding protocol,
		/// typically this is added to the applications UIViewController.
		/// Required for iOS 13+ only, for earlier versions this value must be a nullptr.
		/// using: (void*)CFBridgingRetain(presentationContextProviding)
		/// EOSSDK will release this bridged object when the value is consumed for iOS 13+.
		/// </summary>
		public System.IntPtr PresentationContextProviding { get; set; }

		internal void Set(IOSCredentialsSystemAuthCredentialsOptionsInternal? other)
		{
			if (other != null)
			{
				PresentationContextProviding = other.Value.PresentationContextProviding;
			}
		}

		public void Set(object other)
		{
			Set(other as IOSCredentialsSystemAuthCredentialsOptionsInternal?);
		}
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct IOSCredentialsSystemAuthCredentialsOptionsInternal : ISettable, System.IDisposable
	{
		private int m_ApiVersion;
		private System.IntPtr m_PresentationContextProviding;

		public System.IntPtr PresentationContextProviding
		{
			get
			{
				return m_PresentationContextProviding;
			}

			set
			{
				m_PresentationContextProviding = value;
			}
		}

		public void Set(IOSCredentialsSystemAuthCredentialsOptions other)
		{
			if (other != null)
			{
				m_ApiVersion = AuthInterface.AuthIoscredentialssystemauthcredentialsoptionsApiLatest;
				PresentationContextProviding = other.PresentationContextProviding;
			}
		}

		public void Set(object other)
		{
			Set(other as IOSCredentialsSystemAuthCredentialsOptions);
		}

		public void Dispose()
		{
			Helper.TryMarshalDispose(ref m_PresentationContextProviding);
		}
	}
}