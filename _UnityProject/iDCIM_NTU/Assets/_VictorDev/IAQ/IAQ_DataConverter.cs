namespace VictorDev.IAQ
{

    public abstract class IAQ_DataConverter
    {

        /// <summary>
        /// �ഫ��l�ȳ��
        /// </summary>
        public static void ConvertToDisplayValue(ref IAQ_Data data)
        {
            float displayValue = 0;
            string displayUnitName = "";
            /**
             * �P�_�������A�A�i��ȴ���
             * Switch columnName:
             * {
             * }
             * ****/

            #region [�Ȯɥέ�l�ȻP���]
            displayValue = data.SourceValue;
            displayUnitName = data.SourceUnitName;
            #endregion

            data.SetDisplayData(displayValue, displayUnitName);
        }
    }
}
