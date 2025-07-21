using System.Linq.Expressions;

namespace CSharpBrainTeasers;

public static class Lab
{
    public static T? Run<T>(Expression<Func<T>> expression)
    {
        var compiled = expression.Compile();
        
        // Extract method call information from the expression
        var (className, methodName, parameters, arguments) = ExtractMethodInfo(expression);
        
        Console.WriteLine($"{BOLD}{BLUE}=== Execution Analysis ==={RESET}");

        var methodSignature = FormatMethodSignature(className, methodName, arguments, parameters);
        Console.WriteLine($"{methodSignature}");

        Console.WriteLine();

        try
        {
            Console.WriteLine($"{BOLD}{YELLOW}⏳ Executing...{RESET}");
            var startTime = DateTime.Now;

            T result = compiled();
            Console.WriteLine();

            var endTime = DateTime.Now;
            var duration = endTime - startTime;

            Console.WriteLine($"{BOLD}{GREEN}✅ Execution completed successfully!{RESET}");
            Console.WriteLine($"⏱️  Execution time: {duration.TotalMilliseconds:F2} ms");
            Console.WriteLine($"📋 Result: {FormatResult(result)}({typeof(T).Name}){RESET}");

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{BOLD}{RED}❌ Exception occurred during execution!{RESET}");
            Console.WriteLine($"🚨 Exception: {ex.Message}{RESET} ({ex.GetType().Name})");

            if (ex.InnerException != null)
            {
                Console.WriteLine($"🔗 Inner exception: {ex.InnerException.GetType().Name} - {ex.InnerException.Message}");
            }

            Console.WriteLine($"📍 Stack trace (first few lines):");
            var stackLines = ex.StackTrace?.Split('\n') ?? Array.Empty<string>();
            for (int i = 0; i < Math.Min(3, stackLines.Length); i++)
            {
                Console.WriteLine($"   {stackLines[i].Trim()}");
            }

            return default(T);
        }
        finally
        {
            Console.WriteLine();
            Console.WriteLine($"{BOLD}{BLUE}=== End Analysis ==={RESET}");
            Console.WriteLine();
        }
    }

    public static void Run(Expression<Action> expression)
    {
        var compiled = expression.Compile();
        
        // Extract method call information from the expression
        var (className, methodName, parameters, arguments) = ExtractMethodInfoFromAction(expression);
        
        Console.WriteLine($"{BOLD}{BLUE}=== Execution Analysis ==={RESET}");

        var methodSignature = FormatMethodSignature(className, methodName, arguments, parameters);
        Console.WriteLine($"{methodSignature}");

        Console.WriteLine();

        try
        {
            Console.WriteLine($"{BOLD}{YELLOW}⏳ Executing...{RESET}");
            var startTime = DateTime.Now;

            compiled();
            Console.WriteLine();

            var endTime = DateTime.Now;
            var duration = endTime - startTime;

            Console.WriteLine($"{BOLD}{GREEN}✅ Execution completed successfully!{RESET}");
            Console.WriteLine($"⏱️  Execution time: {duration.TotalMilliseconds:F2} ms");
            Console.WriteLine($"📋 Result: void");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{BOLD}{RED}❌ Exception occurred during execution!{RESET}");
            Console.WriteLine($"🚨 Exception: {ex.Message}{RESET} ({ex.GetType().Name})");

            if (ex.InnerException != null)
            {
                Console.WriteLine($"🔗 Inner exception: {ex.InnerException.GetType().Name} - {ex.InnerException.Message}");
            }

            Console.WriteLine($"📍 Stack trace (first few lines):");
            var stackLines = ex.StackTrace?.Split('\n') ?? Array.Empty<string>();
            for (int i = 0; i < Math.Min(3, stackLines.Length); i++)
            {
                Console.WriteLine($"   {stackLines[i].Trim()}");
            }
        }
        finally
        {
            Console.WriteLine();
            Console.WriteLine($"{BOLD}{BLUE}=== End Analysis ==={RESET}");
            Console.WriteLine();
        }
    }

    private static (string className, string methodName, string[] parameters, object?[] arguments) ExtractMethodInfo<T>(Expression<Func<T>> expression)
    {
        if (expression.Body is MethodCallExpression methodCall)
        {
            var method = methodCall.Method;
            var className = method.DeclaringType?.Name ?? "Unknown";
            var methodName = method.Name;
            
            var parameters = method.GetParameters().Select(p => p.Name ?? "param").ToArray();
            var arguments = methodCall.Arguments.Select(EvaluateExpression).ToArray();
            
            return (className, methodName, parameters, arguments);
        }
        
        return ("Unknown", "Unknown", Array.Empty<string>(), Array.Empty<object?>());
    }

    private static (string className, string methodName, string[] parameters, object?[] arguments) ExtractMethodInfoFromAction(Expression<Action> expression)
    {
        if (expression.Body is MethodCallExpression methodCall)
        {
            var method = methodCall.Method;
            var className = method.DeclaringType?.Name ?? "Unknown";
            var methodName = method.Name;
            
            var parameters = method.GetParameters().Select(p => p.Name ?? "param").ToArray();
            var arguments = methodCall.Arguments.Select(EvaluateExpression).ToArray();
            
            return (className, methodName, parameters, arguments);
        }
        
        return ("Unknown", "Unknown", Array.Empty<string>(), Array.Empty<object?>());
    }

    private static object? EvaluateExpression(Expression expression)
    {
        try
        {
            if (expression is ConstantExpression constant)
                return constant.Value;
                
            var lambda = Expression.Lambda(expression);
            return lambda.Compile().DynamicInvoke();
        }
        catch
        {
            return "?";
        }
    }

    private static string FormatMethodSignature(string className, string methodName, object?[] inputs, string[]? parameterNames = null)
    {
        if (inputs == null || inputs.Length == 0)
        {
            return $"{className}.{methodName}()";
        }

        var parameters = new List<string>();
        for (int i = 0; i < inputs.Length; i++)
        {
            var input = inputs[i];
            var typeName = input?.GetType().Name ?? "object";

            // Use custom parameter name if provided, otherwise use generic name
            var paramName = (parameterNames != null && i < parameterNames.Length)
                ? parameterNames[i]
                : $"param{i + 1}";

            var formattedValue = input switch
            {
                null => "null",
                string str => $"\"{str}\"",
                char c => $"'{c}'",
                _ => input.ToString() ?? "null"
            };

            parameters.Add($"{typeName} {paramName} = {formattedValue}");
        }

        return $"{className}.{methodName}({string.Join(", ", parameters)})";
    }

    private static string FormatResult<T>(T result)
    {
        if (result == null)
            return "null";

        if (result is Array array)
        {
            var elements = new List<string>();
            foreach (var item in array)
            {
                elements.Add(item?.ToString() ?? "null");
            }
            return $"[{string.Join(", ", elements)}] (Length: {array.Length})";
        }

        if (result is System.Collections.IEnumerable enumerable && !(result is string))
        {
            var elements = new List<string>();
            foreach (var item in enumerable)
            {
                elements.Add(item?.ToString() ?? "null");
                if (elements.Count > 10) // Limit display for large collections
                {
                    elements.Add("...");
                    break;
                }
            }
            return $"[{string.Join(", ", elements)}]";
        }

        return result.ToString() ?? "null";
    }

    private const string BOLD = "\u001b[1m";
    private const string RESET = "\u001b[0m";
    private const string GREEN = "\u001b[32m";
    private const string RED = "\u001b[31m";
    private const string YELLOW = "\u001b[33m";
    private const string BLUE = "\u001b[34m";
    private const string CYAN = "\u001b[36m";
    private const string GRAY = "\u001b[90m";
    private const string PURPLE = "\u001b[35m";
}

