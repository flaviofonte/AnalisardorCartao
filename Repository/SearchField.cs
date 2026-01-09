namespace AnalisardorCartao.Repository
{
    public class SearchField
    {
        public string Name { get; set; }
        public object @Value { get; set; }
        public TipoOperacaoEnum Operator { get; set; }

        public SearchField(string Name, object @Value, TipoOperacaoEnum @operator)
        {
            this.Name = Name;
            this.@Value = @Value;
            this.Operator = @operator;
        }
    }
}
