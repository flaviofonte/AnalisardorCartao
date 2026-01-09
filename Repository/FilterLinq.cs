using System;
using System.Linq.Expressions;

namespace AnalisardorCartao.Repository
{
    public class FilterLinq<T>
    {
        public static Expression<Func<T, bool>> GetWherePredicate(params SearchField[] SearchFieldList)
        {
            ParameterExpression pe = Expression.Parameter(typeof(T), typeof(T).Name);
            Expression combined = null;
            if (SearchFieldList != null)
            {
                foreach (var fieldItem in SearchFieldList)
                {
                    var columnNameProperty = Expression.Property(pe, fieldItem.Name);
                    Type propertyType = columnNameProperty.Type;
                    Type targetType = null;

                    object rawValue = fieldItem.Value;
                    object typedValue = null;

                    if (rawValue != null)
                    {
                        targetType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
                        if (rawValue.GetType() != targetType)
                        {
                            try
                            {
                                typedValue = Convert.ChangeType(rawValue, targetType);
                            }
                            catch
                            {
                                // fallback: tentar suportar enums
                                if (targetType.IsEnum && rawValue is string v)
                                    typedValue = Enum.Parse(targetType, v);
                                else
                                    typedValue = rawValue;
                            }
                        }
                        else
                        {
                            typedValue = rawValue;
                        }
                    }

                    Expression columnValue;
                    if (typedValue == null)
                    {
                        columnValue = Expression.Constant(null, propertyType);
                    }
                    else
                    {
                        // cria constante com o tipo subjacente e depois converte para o propertyType (suporta Nullable<T>)
                        var constExpr = Expression.Constant(typedValue, targetType);
                        if (propertyType != targetType)
                            columnValue = Expression.Convert(constExpr, propertyType); // ex: DateTime -> DateTime?
                        else
                            columnValue = constExpr;
                    }

                    TipoOperacaoEnum operacao = fieldItem.Operator;
                    Expression e1;
                    switch (operacao)
                    {
                        case TipoOperacaoEnum.MAIORIGUAL:
                            e1 = Expression.GreaterThanOrEqual(columnNameProperty, columnValue);
                            break;
                        case TipoOperacaoEnum.DIFERENTE:
                            e1 = Expression.NotEqual(columnNameProperty, columnValue);
                            break;
                        case TipoOperacaoEnum.MAIORQUE:
                            e1 = Expression.GreaterThan(columnNameProperty, columnValue);
                            break;
                        case TipoOperacaoEnum.MENORQUE:
                            e1 = Expression.LessThan(columnNameProperty, columnValue);
                            break;
                        case TipoOperacaoEnum.MENORIGUAL:
                            e1 = Expression.LessThanOrEqual(columnNameProperty, columnValue);
                            break;
                        case TipoOperacaoEnum.NULO:
                            e1 = Expression.Equal(columnNameProperty, Expression.Constant(null, propertyType));
                            break;
                        case TipoOperacaoEnum.NAONULO:
                            e1 = Expression.NotEqual(columnNameProperty, Expression.Constant(null, propertyType));
                            break;
                        default:
                            e1 = Expression.Equal(columnNameProperty, columnValue);
                            break;
                    }

                    combined = combined == null ? e1 : Expression.AndAlso(combined, e1);
                }
            }

            if (combined == null)
                combined = Expression.Constant(true); // evita Lambda com body null

            return Expression.Lambda<Func<T, bool>>(combined, pe);
        }
    }
}
