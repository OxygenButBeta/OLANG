using OLANG.Core;

namespace OLANG.Syntax;

public class Lexer {
    readonly string raw;
    int position;

    public Lexer(string raw) {
        this.raw = raw;
        position = 0;
    }

    public DiagnosticBag Diagnostics { get; } = new();
    char Current => position >= raw.Length ? '\0' : raw[position];
    char Peek(int offset) => (position + offset >= raw.Length) ? '\0' : raw[position + offset];
    bool IsAlpha(char c) => char.IsLetter(c) || c == '_';
    bool IsAlphaNumeric(char c) => char.IsLetterOrDigit(c) || c == '_';

    public Token NextToken() {
        if (position >= raw.Length)
            return new Token(TokenType.EOF, "\0", null, position);
        if (IsAlpha(Current)) {
            var start = position;
            while (IsAlphaNumeric(Current)) position++;

            var text = raw.Substring(start, position - start);
            return new Token(TokenType.Identifier, text, null, start);
        }

        if (char.IsDigit(Current)) {
            var start = position;
            while (char.IsDigit(Current))
                position++;

            var length = position - start;
            var text = this.raw.Substring(start, length);
            int.TryParse(text, out var value);
            return new Token(TokenType.Number, text, value, start);
        }

        if (char.IsLetter(Current)) {
            var start = position;
            while (char.IsLetter(Current)) position++;

            var text = raw.Substring(start, position - start);
            return new Token(TokenType.Identifier, text, null, start);
        }

        if (char.IsWhiteSpace(Current)) {
            while (char.IsWhiteSpace(Current))
                position++;

            return NextToken();
        }

        switch (Current) {
            case '!':
                if (Peek(1) == '=') {
                    position += 2;
                    return new Token(TokenType.BangEquals, "!=", null, position - 2);
                }

                return new Token(TokenType.Bang, "!", null, position++);

            case '=':
                if (Peek(1) == '=') {
                    position += 2;
                    return new Token(TokenType.EqualsEquals, "==", null, position - 2);
                }

                return new Token(TokenType.Equals, "=", null, position++);

            case '<':
                if (Peek(1) == '=') {
                    position += 2;
                    return new Token(TokenType.LessOrEquals, "<=", null, position - 2);
                }

                return new Token(TokenType.Less, "<", null, position++);

            case '>':
                if (Peek(1) == '=') {
                    position += 2;
                    return new Token(TokenType.GreaterOrEquals, ">=", null, position - 2);
                }

                return new Token(TokenType.Greater, ">", null, position++);
            case '+':
                return new Token(TokenType.Plus, "+", null, position++);
            case '-':
                return new Token(TokenType.Minus, "-", null, position++);
            case '*':
                return new Token(TokenType.Star, "*", null, position++);
            case '/':
                return new Token(TokenType.Slash, "/", null, position++);
            case '(':
                return new Token(TokenType.OpenParen, "(", null, position++);
            case ')':
                return new Token(TokenType.CloseParen, ")", null, position++);

            case ',':
                return new Token(TokenType.Comma, ",", null, position++);
            default:
                Diagnostics.ReportInvalidCharacter(position, Current);
                return new Token(TokenType.BadToken, raw.Substring(position++, 1), null, position - 1);
        }
    }
}