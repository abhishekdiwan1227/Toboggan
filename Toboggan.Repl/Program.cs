using Toboggan;
using Toboggan.Bindings;
using Toboggan.Symbols;
using Toboggan.Syntax;

public class Program
{
    private static void Main(string[] args)
    {
        var variables = new Dictionary<VariableSymbol, object?>();
        var showTree = true;
        while (true)
        {
            try
            {
                Console.Write("> ");
                var line = Console.ReadLine();

                if (line == "#showTree")
                {
                    showTree = !showTree;
                    continue;
                }

                if (line == "#clear")
                {
                    Console.Clear();
                    continue;
                }

                if (string.IsNullOrWhiteSpace(line)) continue;

                var parser = new Parser(line);
                var tree = parser.Parse();

                var binder = new Binder(variables);

                var boundTree = binder.Bind(tree.Expression);

                var evaluator = new Evaluator(boundTree, variables);
                var result = evaluator.Evaluate();

                if (showTree) Print(tree.Expression);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(result);
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        void Print(SyntaxNode root, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "└─" : "├─";

            Console.Write(indent);
            Console.Write(marker);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(root.Kind);
            Console.ResetColor();

            if (root is SyntaxToken t && t.Value is not null)
            {
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(string.Format("({0})", t.Value));
                Console.ResetColor();
            }

            Console.WriteLine();

            var children = root.Children;

            var lastChild = children.LastOrDefault();
            indent += isLast ? "    " : "│   ";
            foreach (var child in children)
            {
                Print(child, indent, child == lastChild);
            }
        }
    }
}