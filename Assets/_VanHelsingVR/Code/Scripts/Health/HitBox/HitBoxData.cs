using System.Collections.Generic;

namespace _VanHelsingVR.Health
{
    [System.Serializable]
    /*
     * Esta clase es El contenedor de la informacion que contendrá la HurtBox
     * El daño, el knockback, multiplicadores etc
     */
    public struct HitBoxData
    {
        // Un Diccionario que basa sus llaves en un enumerador y sus objetos en los datos;
        private Dictionary<HitBoxDataType, HitBoxDataAttribute> _dataDictionary;

        private Dictionary<HitBoxDataType, HitBoxDataAttribute> DataDictionary
            => _dataDictionary ??= new Dictionary<HitBoxDataType, HitBoxDataAttribute>();
        

        public HitBoxDataAttribute GetHitBoxData(HitBoxDataType dataType)
        {
            //Si existe la informacion devuelvela
            if (DataDictionary.TryGetValue(dataType, out HitBoxDataAttribute data)) return data;
            
            //Si no, crea el valor default y retornala
            DataDictionary.Add(dataType, default(HitBoxDataAttribute));
            data = DataDictionary[dataType];
            return data;
        }

        public void SetHitBoxData(HitBoxDataType dataType, HitBoxDataAttribute newDataAttribute)
        {
            if (DataDictionary.TryGetValue(dataType, out _))
            {
                DataDictionary[dataType] = newDataAttribute;
            }
            else
            {
                DataDictionary.Add(dataType, newDataAttribute);
            }
        }
    }
}