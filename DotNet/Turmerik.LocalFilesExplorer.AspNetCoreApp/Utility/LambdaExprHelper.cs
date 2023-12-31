using System.Linq.Expressions;
using System.Reflection;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public interface ILambdaExprHelper
    {
        MemberInfo GetMember<TMember>(Expression<Func<TMember>> memberLambda);
        string GetMemberName<TMember>(Expression<Func<TMember>> memberLambda);
        PropertyInfo GetProp<TProperty>(Expression<Func<TProperty>> propertyLambda);
        string GetPropName<TProperty>(Expression<Func<TProperty>> propertyLambda);
        MethodInfo GetMethod<TProperty>(Expression<Func<TProperty>> methodLambda);
        string GetMethodName<TProperty>(Expression<Func<TProperty>> methodLambda);

        MemberInfo Member<TSource, TProperty>(
            Expression<Func<TSource, TProperty>> memberLambda,
            Type type = null, bool checkReflectedType = false);

        string MemberName<TSource, TProperty>(
            Expression<Func<TSource, TProperty>> memberLambda,
            Type type = null, bool checkReflectedType = false);

        PropertyInfo Prop<TSource, TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda,
            Type type = null, bool checkReflectedType = false);

        string PropName<TSource, TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda,
            Type type = null, bool checkReflectedType = false);

        MethodInfo Method<TSource, TProperty>(Expression<Func<TSource, TProperty>> methodLambda,
            Type type = null, bool checkReflectedType = false);

        string MethodName<TSource, TProperty>(Expression<Func<TSource, TProperty>> methodLambda,
            Type type = null, bool checkReflectedType = false);
    }

    public interface ILambdaExprHelper<TSource>
    {
        MemberInfo Member<TMember>(
            Expression<Func<TSource, TMember>> memberLambda,
            Type type = null, bool checkReflectedType = false);

        string MemberName<TMember>(
            Expression<Func<TSource, TMember>> memberLambda,
            Type type = null, bool checkReflectedType = false);

        PropertyInfo Prop<TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda,
            Type type = null, bool checkReflectedType = false);

        string PropName<TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda,
            Type type = null, bool checkReflectedType = false);

        MethodInfo Method<TProperty>(Expression<Func<TSource, TProperty>> methodLambda,
            Type type = null, bool checkReflectedType = false);

        string MethodName<TProperty>(Expression<Func<TSource, TProperty>> methodLambda,
            Type type = null, bool checkReflectedType = false);
    }

    public interface ILambdaExprHelperFactory
    {
        ILambdaExprHelper<TSource> GetHelper<TSource>();
    }

    public class LambdaExprHelper : ILambdaExprHelper
    {
        public MemberInfo GetMember<TMember>(Expression<Func<TMember>> memberLambda)
        {
            MemberExpression member = memberLambda.Body as MemberExpression;
            ThrowErrIfReq(() => member == null);

            MemberInfo memberInfo = member.Member;
            return memberInfo;
        }

        public string GetMemberName<TMember>(Expression<Func<TMember>> memberLambda)
        {
            MemberInfo memberInfo = GetMember(memberLambda);
            string name = memberInfo.Name;

            return name;
        }

        public PropertyInfo GetProp<TProperty>(Expression<Func<TProperty>> propertyLambda)
        {
            MemberInfo memberInfo = GetMember(propertyLambda);

            PropertyInfo propInfo = memberInfo as PropertyInfo;
            ThrowErrIfReq(() => propInfo == null);

            return propInfo;
        }

        public string GetPropName<TProperty>(Expression<Func<TProperty>> propertyLambda)
        {
            PropertyInfo propInfo = GetProp(propertyLambda);
            string name = propInfo.Name;

            return name;
        }

        public MethodInfo GetMethod<TProperty>(Expression<Func<TProperty>> methodLambda)
        {
            MethodCallExpression member = methodLambda.Body as MethodCallExpression;
            ThrowErrIfReq(() => member == null);

            MethodInfo methodInfo = member.Method;
            ThrowErrIfReq(() => methodInfo == null);

            return methodInfo;
        }

        public string GetMethodName<TProperty>(Expression<Func<TProperty>> methodLambda)
        {
            MethodInfo methodInfo = GetMethod(methodLambda);
            string name = methodInfo.Name;

            return name;
        }

        public MemberInfo Member<TSource, TProperty>(
            Expression<Func<TSource, TProperty>> memberLambda,
            Type type = null, bool checkReflectedType = false)
        {
            type = type ?? typeof(TSource);

            MemberExpression member = memberLambda.Body as MemberExpression;
            ThrowErrIfReq(() => member == null, memberLambda);

            MemberInfo memberInfo = member.Member;

            ThrowErrIfReq(
                () => checkReflectedType && !memberInfo.ReflectedType.IsAssignableFrom(type),
                memberLambda,
                () => string.Format(
                    "Expression '{0}' refers to a property that is not from type {1}.",
                    memberLambda.ToString(),
                    type));

            return memberInfo;
        }

        public string MemberName<TSource, TProperty>(
            Expression<Func<TSource, TProperty>> memberLambda,
            Type type = null, bool checkReflectedType = false)
        {
            MemberInfo memberInfo = Member(memberLambda, type, checkReflectedType);
            string name = memberInfo.Name;

            return name;
        }

        public PropertyInfo Prop<TSource, TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda,
            Type type = null, bool checkReflectedType = false)
        {
            type = type ?? typeof(TSource);
            MemberInfo memberInfo = Member(propertyLambda, type, checkReflectedType);

            PropertyInfo propInfo = memberInfo as PropertyInfo;
            ThrowErrIfReq(() => propInfo == null, propertyLambda);

            ThrowErrIfReq(
                () => checkReflectedType && !propInfo.ReflectedType.IsAssignableFrom(type),
                propertyLambda,
                () => string.Format(
                    "Expression '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));

            return propInfo;
        }

        public string PropName<TSource, TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda,
            Type type = null, bool checkReflectedType = false)
        {
            PropertyInfo propInfo = Prop(propertyLambda, type, checkReflectedType);
            string name = propInfo.Name;

            return name;
        }

        public MethodInfo Method<TSource, TProperty>(Expression<Func<TSource, TProperty>> methodLambda, Type type = null, bool checkReflectedType = false)
        {
            type = type ?? typeof(TSource);

            MethodCallExpression member = methodLambda.Body as MethodCallExpression;
            ThrowErrIfReq(() => member == null, methodLambda);

            MethodInfo methodInfo = member.Method;
            ThrowErrIfReq(() => methodInfo == null, methodLambda);

            ThrowErrIfReq(
                () => checkReflectedType && !methodInfo.ReflectedType.IsAssignableFrom(type),
                methodLambda,
                () => string.Format(
                    "Expression '{0}' refers to a method that is not from type {1}.",
                    methodLambda.ToString(),
                    type));

            return methodInfo;
        }

        public string MethodName<TSource, TProperty>(Expression<Func<TSource, TProperty>> methodLambda, Type type = null, bool checkReflectedType = false)
        {
            MethodInfo methodInfo = Method(methodLambda, type, checkReflectedType);
            string name = methodInfo.Name;

            return name;
        }

        private static void ThrowErrIfReq(
            Func<bool> condition,
            Func<string> messageFactory = null)
        {
            if (condition())
            {
                string message = messageFactory?.Invoke() ?? "Invalid expression argument";
                throw new ArgumentException(message);
            }
        }

        private static T ThrowErr<T, TSource, TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda,
            Func<string> messageFactory = null)
        {
            string message = messageFactory?.Invoke() ?? string.Format(
                "Expression '{0}' does not refer to a property.",
                propertyLambda.ToString());

            throw new ArgumentException(message);
        }

        private static void ThrowErrIfReq<TSource, TProperty>(
            Func<bool> condition,
            Expression<Func<TSource, TProperty>> propertyLambda,
            Func<string> messageFactory = null)
        {
            if (condition())
            {
                ThrowErr<object, TSource, TProperty>(
                    propertyLambda, messageFactory);
            }
        }
    }

    public class LambdaExprHelper<TSource> : ILambdaExprHelper<TSource>
    {
        private readonly ILambdaExprHelper innerHelper;

        public LambdaExprHelper(ILambdaExprHelper innerHelper)
        {
            this.innerHelper = innerHelper ?? throw new ArgumentNullException(nameof(innerHelper));
        }

        public MemberInfo Member<TMember>(
            Expression<Func<TSource, TMember>> memberLambda,
            Type type = null,
            bool checkReflectedType = false) => innerHelper.Member(
                memberLambda,
                type,
                checkReflectedType);

        public string MemberName<TMember>(
            Expression<Func<TSource, TMember>> memberLambda,
            Type type = null,
            bool checkReflectedType = false) => innerHelper.MemberName(
                memberLambda,
                type,
                checkReflectedType);

        public MethodInfo Method<TProperty>(
            Expression<Func<TSource, TProperty>> methodLambda,
            Type type = null,
            bool checkReflectedType = false) => innerHelper.Method(
                methodLambda,
                type,
                checkReflectedType);

        public string MethodName<TProperty>(
            Expression<Func<TSource, TProperty>> methodLambda,
            Type type = null,
            bool checkReflectedType = false) => innerHelper.MethodName(
                methodLambda,
                type,
                checkReflectedType);

        public string PropName<TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda,
            Type type = null,
            bool checkReflectedType = false) => innerHelper.PropName(
                propertyLambda,
                type,
                checkReflectedType);

        public PropertyInfo Prop<TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda,
            Type type = null,
            bool checkReflectedType = false) => innerHelper.Prop(
                propertyLambda,
                type,
                checkReflectedType);
    }

    public class LambdaExprHelperFactory : ILambdaExprHelperFactory
    {
        private readonly ILambdaExprHelper lambdaExprHelper;

        public LambdaExprHelperFactory(ILambdaExprHelper lambdaExprHelper)
        {
            this.lambdaExprHelper = lambdaExprHelper ?? throw new ArgumentNullException(nameof(lambdaExprHelper));
        }

        public ILambdaExprHelper<TSource> GetHelper<TSource>() => new LambdaExprHelper<TSource>(lambdaExprHelper);
    }
}
