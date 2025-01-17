using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Xrm.Sdk.Query;

namespace FakeXrmEasy.Query
{
    internal static partial class ConditionExpressionExtensions
    {
        internal static Expression ToContainsExpression(this TypedConditionExpression tc, Expression getAttributeValueExpr, Expression containsAttributeExpr)
        {
            var c = tc.CondExpression;

            //Append a ´%´at the end of each condition value
            var computedCondition = new ConditionExpression(c.AttributeName, c.Operator, c.Values.Select(x => "%" + x.ToString() + "%").ToList());
            var computedTypedCondition = new TypedConditionExpression(computedCondition, tc.QueryExpression);
            computedTypedCondition.AttributeType = tc.AttributeType;

            return computedTypedCondition.ToLikeExpression(getAttributeValueExpr, containsAttributeExpr);

        }
    }
}
