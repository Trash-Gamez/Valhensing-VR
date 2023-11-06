using System.Collections.Generic;

namespace _VanHelsingVR.Health
{
    [System.Serializable]
    /*
     * Esta clase es El contenedor de la informacion que contendrá la HurtBox
     * El daño, el knockback, multiplicadores etc
     */
    public struct HitBoxDataContainer
    {
        // Un Diccionario que basa sus llaves en un enumerador y sus objetos en los datos;
        private Dictionary<HitBoxDataType, HitBoxData> _dataDictionary;

        private Dictionary<HitBoxDataType, HitBoxData> DataDictionary
            => _dataDictionary ??= new Dictionary<HitBoxDataType, HitBoxData>();
        

        public HitBoxData GetHitBoxData(HitBoxDataType dataType)
        {
            //Si existe la informacion devuelvela
            if (DataDictionary.TryGetValue(dataType, out HitBoxData data)) return data;
            
            //Si no, crea el valor default y retornala
            DataDictionary.Add(dataType, default(HitBoxData));
            data = DataDictionary[dataType];
            return data;
        }

        public void SetHitBoxData(HitBoxDataType dataType, HitBoxData newData)
        {
            if (DataDictionary.TryGetValue(dataType, out _))
            {
                DataDictionary[dataType] = newData;
            }
            else
            {
                DataDictionary.Add(dataType, newData);
            }
        }
    }
}