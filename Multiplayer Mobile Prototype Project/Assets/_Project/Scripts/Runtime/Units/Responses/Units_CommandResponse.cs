namespace Core.Runtime.Units.Responses
{
    using Types;
    using Providers;
    
    public class Units_CommandResponse
    {
        protected Units_CommandProvider m_provider;

        public virtual void Init(Units_CommandProvider provider) => m_provider = provider;

        public virtual void SendCommand(Unit unit, object commandObject, params Unit_CommandType[] commandTypes){}
        
        public virtual void SendCommand(Unit unit, Unit_CommandType commandType, object commandObject){}
    }
}