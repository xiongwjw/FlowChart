using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;

namespace Lassalle.Flow
{
	/// <summary>
	/// Lassalle ��ժҪ˵����
	/// </summary>
	internal class af : System.ComponentModel.Component
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public af(System.ComponentModel.IContainer container)
		{
			///
			/// Windows.Forms ��׫д�����֧���������
			///
			container.Add(this);
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		public af()
		{
			///
			/// Windows.Forms ��׫д�����֧���������
			///
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary> 
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region �����������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{

        }
		#endregion
	}
}
