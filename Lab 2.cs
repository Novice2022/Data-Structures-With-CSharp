using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Laboratory
{
    internal class Lab2
    {
        public static void Run()
        {
            string[] sigma = { "0", "1" };
            string[] nfaStates = { "q0", "q1", "q2" };
            string[] nfaAcceptingStates = { "q2" };
            string nfaStartState = "q0";

            Rd[] nfaTransitions = {
                new Rd("0", "q0", "q0"), // Остаться в q0 при чтении "0"
                new Rd("1", "q0", "q1"), // Перейти в q1 при чтении "1" в состоянии q0
                new Rd("1", "q1", "q0"), // Остаться в q1 при чтении "0"
                new Rd("0", "q1", "q2"), // Перейти в принимающее состояние q2 при чтении "1" в состоянии q1
                new Rd("0", "q2", "q1"), // Оставаться в q2 при чтении "0"
                new Rd("1", "q2", "q2")  // Оставаться в q2 при чтении "1"
            };

            NFA nfa = new NFA(sigma, nfaStates, nfaAcceptingStates, nfaStartState, nfaTransitions);
            Console.WriteLine(nfa);


            //string[] alfabet = { "0", "1" };
            //string[] states = { "q0", "q1" };
            //string[] tstates = { "q1" };
            //string startState = "q0";

            //Rd[] aDelt =
            //{
            //    new Rd("1", "q0", "q1"),
            //    new Rd("0", "q0", "q0"),
            //    new Rd("1", "q1", "q1"),
            //    new Rd("0", "q1", "q0")
            //};

            //DFA dfa = new DFA(sigma, nfaStates, nfaAcceptingStates, nfaStartState, nfaTransitions);
            //dfa = DFA.ConvertNFAtoDFA(nfa);

            //Console.WriteLine(dfa);
            //Console.WriteLine(dfa.ToRegex());
            //bool areEquivalent0 = AutomatonEquivalenceChecker.AreEquivalent(dfa, nfa);
            //Console.WriteLine($"Эквивалентны ли ДКА и НКА? {areEquivalent0}");


            //string regexPattern = dfa.ToRegex();
            //bool areEquivalent1 = AutomatonEquivalenceChecker.AreEquivalent(dfa, regexPattern);
            //Console.WriteLine($"Эквивалентны ли ДКА и регулярное выражение? {areEquivalent1}");

            string regexPattern = nfa.ToRegex();
            Console.WriteLine(nfa.ToRegex());

            bool areEquivalent = AutomatonEquivalenceChecker.AreEquivalent(nfa, regexPattern);
            
            //regexPattern = RegexSimplifier.SimplifyRegex(regexPattern);
            ////Console.WriteLine(regexPattern);
            //regexPattern = RegexSimplifier.SimplifyRegex(regexPattern);
            //Console.WriteLine(regexPattern);
            
            Console.WriteLine($"\nЭквивалентны ли НКА и регулярное выражение? {areEquivalent}");
        }
    }

    public class Rd
    {
        public string c, q, rez;
        public Rd(string cp, string qp, string rezp)
        {
            c = cp;
            q = qp;
            rez = rezp;
        }
    }

    public class DFA
    {
        public string[] Sigma, Q, T;
        public string S;
        public Rd[] ADelta;

        public DFA(string[] sigma, string[] q, string[] t, string s, Rd[] aDelta)
        {
            Sigma = sigma;
            Q = q;
            T = t;
            S = s;
            ADelta = aDelta;
        }

        public string Delta(string c, string q)
        {
            foreach (Rd t in ADelta)
                if (t.c == c && t.q == q) return t.rez;

            return " ";
        }

        public bool Accept(string x)
        {
            string current = S; // Начальное состояние

            foreach (char c in x)
                current = Delta("" + c, current); // Переход по символам

            return T.Contains(current); // Проверка, является ли текущее состояние принимающим
        }
        public static DFA ConvertNFAtoDFA(NFA nfa)
        {
            List<string> dfaStates = new List<string>(); // Список состояний ДКА
            List<Rd> dfaTransitions = new List<Rd>(); // Переходы ДКА
            List<string> dfaFinalStates = new List<string>(); // Принимающие состояния ДКА

            // Начальное состояние ДКА — это множество, содержащее начальное состояние НКА
            HashSet<string> startStateSet = new HashSet<string> { nfa.S };
            Queue<HashSet<string>> unprocessedStates = new Queue<HashSet<string>>();
            Dictionary<HashSet<string>, string> stateNameMap = new Dictionary<HashSet<string>, string>(HashSet<string>.CreateSetComparer());

            // Присваиваем новому множеству состояний уникальное имя (например, "q0")
            string startStateName = "q0";
            dfaStates.Add(startStateName);
            stateNameMap[startStateSet] = startStateName;
            unprocessedStates.Enqueue(startStateSet);

            // Начальное состояние ДКА
            string dfaStartState = startStateName;

            // Пока есть необработанные множества состояний
            while (unprocessedStates.Count > 0)
            {
                HashSet<string> currentSet = unprocessedStates.Dequeue();
                string currentStateName = stateNameMap[currentSet];

                // Для каждого символа алфавита
                foreach (string symbol in nfa.Sigma)
                {
                    HashSet<string> nextSet = new HashSet<string>();

                    // Для каждого состояния в текущем множестве
                    foreach (string state in currentSet)
                    {
                        List<string> nextStates = nfa.Delta(symbol, state);
                        nextSet.UnionWith(nextStates);
                    }

                    if (nextSet.Count > 0)
                    {
                        // Если это множество состояний еще не встречалось, добавляем его
                        if (!stateNameMap.ContainsKey(nextSet))
                        {
                            string nextStateName = "q" + stateNameMap.Count;
                            stateNameMap[nextSet] = nextStateName;
                            dfaStates.Add(nextStateName);
                            unprocessedStates.Enqueue(nextSet);
                        }

                        // Добавляем переход в ДКА
                        string nextStateNameMapped = stateNameMap[nextSet];
                        dfaTransitions.Add(new Rd(symbol, currentStateName, nextStateNameMapped));
                    }
                }
            }

            // Определяем принимающие состояния ДКА
            foreach (var stateSet in stateNameMap)
                if (stateSet.Key.Any(state => nfa.T.Contains(state)))
                    dfaFinalStates.Add(stateSet.Value);

            // Возвращаем новый ДКА
            return new DFA(nfa.Sigma, dfaStates.ToArray(), dfaFinalStates.ToArray(), dfaStartState, dfaTransitions.ToArray());
        }

        public string ToRegex()
        {
            // Создаем новые начальное и конечное состояния
            string newStart = "newStart";
            string newFinal = "newFinal";

            // Переходы: добавляем epsilon-переход из нового начального состояния в оригинальное
            List<Rd> augmentedTransitions = new List<Rd>(ADelta);
            augmentedTransitions.Add(new Rd("", newStart, S)); // ε-переход
            foreach (string finalState in T)
                augmentedTransitions.Add(new Rd("", finalState, newFinal)); // ε-переходы из старых конечных в новое конечное

            // Множество состояний с новым начальным и конечным
            List<string> augmentedStates = new List<string>(Q);
            augmentedStates.Add(newStart);
            augmentedStates.Add(newFinal);

            // Переходы между состояниями теперь можно представлять как регулярные выражения
            Dictionary<(string, string), string> transitions = new Dictionary<(string, string), string>();

            // Проход всех переходов и добавление их в словарь
            foreach (Rd transition in augmentedTransitions)
            {
                var key = (transition.q, transition.rez);
                // Объединение простых параллельных переходов
                if (transitions.ContainsKey(key))
                    transitions[key] += "|" + transition.c; // Объединяем переходы через OR (|)
                else
                    transitions[key] = transition.c;
            }

            // Удаляем состояния одно за другим (кроме начального и конечного)
            foreach (string state in augmentedStates.ToArray())
            {
                if (state == newStart || state == newFinal) continue; // Пропускаем новые начальное и конечное

                // Обрабатываем переходы через удаляемое состояние
                foreach (var fromState in augmentedStates)
                    foreach (var toState in augmentedStates)
                    {
                        if (fromState == state || toState == state) continue; // если состояние для удаления в fromState или toState пропускаем их

                        string ToState = transitions.ContainsKey((fromState, state)) ? transitions[(fromState, state)] : null;
                        string Loop = transitions.ContainsKey((state, state)) ? transitions[(state, state)] : null;
                        string FromState = transitions.ContainsKey((state, toState)) ? transitions[(state, toState)] : null;

                        if (ToState != null && FromState != null)
                        {
                            // Если есть путь через удаляемое состояние, обновляем переходы
                            string newTransition = ToState;
                            // добавление в переход замыкания
                            if (Loop != null) newTransition += "(" + Loop + ")*";
                            // котенация перехода и перехода из текущего состояния
                            newTransition += FromState;

                            // Проверка на существование параллельного перехода текущему состоянию
                            var key = (fromState, toState);
                            if (transitions.ContainsKey(key))
                                transitions[key] += "|" + newTransition; // Объединяем с существующим переходом
                            else
                                transitions[key] = newTransition;
                        }
                    }

                // Удаляем все переходы, связанные с удаляемым состоянием
                transitions = transitions
                    .Where(t => t.Key.Item1 != state && t.Key.Item2 != state)
                    .ToDictionary(t => t.Key, t => t.Value);
            }

            // После удаления всех состояний остались только переход между начальным и конечным
            return transitions.ContainsKey((newStart, newFinal)) ? transitions[(newStart, newFinal)] : "";
        }

        public override string ToString()
        {
            // Форматируем вывод всех составляющих ДКА
            string sigmaStr = string.Join(", ", Sigma);
            string qStr = string.Join(", ", Q);
            string tStr = string.Join(", ", T);

            // Начальное состояние
            string startStr = $"Начальное состояние: {S}";

            // Формируем таблицу переходов
            string deltaStr = "Переходы:\n";
            foreach (Rd transition in ADelta)
                deltaStr += $"δ({transition.q}, {transition.c}) -> {transition.rez}\n";

            // Объединяем все части
            return $"Алфавит: {{{sigmaStr}}}\n" +
                   $"Состояния: {{{qStr}}}\n" +
                   $"{startStr}\n" +
                   $"Принимающие состояния: {{{tStr}}}\n" +
                   deltaStr;
        }
    }

    public class NFA
    {
        public string[] Sigma, Q, T; // Алфавит, множество состояний, принимающие состояния
        public string S; // Начальное состояние
        public Rd[] ADelta; // Функция переходов

        public NFA(string[] sigma, string[] q, string[] t, string s, Rd[] aDelta)
        {
            Sigma = sigma;
            Q = q;
            T = t;
            S = s;
            ADelta = aDelta;
        }

        public List<string> Delta(string c, string q)
        {
            List<string> nextStates = new List<string>();
            
            foreach (Rd t in ADelta)
                if (t.q == q && (t.c == c || t.c == ""))
                    nextStates.Add(t.rez);

            return nextStates;
        }

        public HashSet<string> EpsilonClosure(string q)
        {
            var closure = new HashSet<string> { q };
            var stack = new Stack<string>();
            stack.Push(q);

            while (stack.Count > 0)
            {
                var state = stack.Pop();
                foreach (var next in Delta("", state))
                    if (!closure.Contains(next))
                    {
                        closure.Add(next);
                        stack.Push(next);
                    }
            }

            return closure;
        }

        // Перегрузка для множества начальных состояний
        public HashSet<string> EpsilonClosure(HashSet<string> states)
        {
            var closure = new HashSet<string>();

            foreach (var state in states)
                closure.UnionWith(EpsilonClosure(state));
            
            return closure;
        }

        public bool Accept(string x)
        {
            // Начинаем с ε-замыкания начального состояния
            HashSet<string> currentStates = EpsilonClosure(new HashSet<string> { S });

            foreach (char c in x)
            {
                HashSet<string> nextStates = new HashSet<string>();

                // Переходим по каждому состоянию в текущем множестве состояний по символу строки
                foreach (string state in currentStates)
                    nextStates.UnionWith(Delta("" + c, state));

                // Если нет состояний, переходим к следующему символу
                if (nextStates.Count == 0) return false;

                // Добавляем ε-замыкание для новых состояний
                currentStates = EpsilonClosure(nextStates);
            }

            // Проверяем, есть ли среди текущих состояний хотя бы одно принимающее
            return currentStates.Any(state => T.Contains(state));
        }

        public override string ToString()
        {
            // Форматируем вывод всех составляющих НКА
            string sigmaStr = string.Join(", ", Sigma);
            string qStr = string.Join(", ", Q);
            string tStr = string.Join(", ", T);

            // Начальное состояние
            string startStr = $"Начальное состояние: {S}";

            // Формируем таблицу переходов
            string deltaStr = "Переходы:\n";
            foreach (Rd transition in ADelta)
                deltaStr += $"δ({transition.q}, {transition.c}) -> {transition.rez}\n";

            // Объединяем все части
            return $"Алфавит: {{{sigmaStr}}}\n" +
                   $"Состояния: {{{qStr}}}\n" +
                   $"{startStr}\n" +
                   $"Принимающие состояния: {{{tStr}}}\n" +
                   deltaStr;
        }

        public string ToRegex()
        {
            // Создаем новые начальное и конечное состояния
            string newStart = "newStart";
            string newFinal = "newFinal";

            // Переходы: добавляем epsilon-переход из нового начального состояния в оригинальное
            List<Rd> augmentedTransitions = new List<Rd>(ADelta);
            augmentedTransitions.Add(new Rd("", newStart, S)); // ε-переход
            foreach (string finalState in T)
                augmentedTransitions.Add(new Rd("", finalState, newFinal)); // ε-переходы из старых конечных в новое конечное

            // Множество состояний с новым начальным и конечным
            List<string> augmentedStates = new List<string>(Q);
            augmentedStates.Add(newStart);
            augmentedStates.Add(newFinal);

            // Переходы между состояниями теперь можно представлять как регулярные выражения
            Dictionary<(string, string), string> transitions = new Dictionary<(string, string), string>();

            // Проход всех переходов и добавление их в словарь
            foreach (Rd transition in augmentedTransitions)
            {
                var key = (transition.q, transition.rez);
                // Объединение простых параллельных переходов
                if (transitions.ContainsKey(key))
                    transitions[key] += "|" + transition.c; // Объединяем переходы через OR (|)
                else
                    transitions[key] = transition.c;
            }

            // Удаляем состояния одно за другим (кроме начального и конечного)
            foreach (string state in augmentedStates.ToArray())
            {
                if (state == newStart || state == newFinal) continue; // Пропускаем новые начальное и конечное

                // Обрабатываем переходы через удаляемое состояние
                foreach (var fromState in augmentedStates)
                    foreach (var toState in augmentedStates)
                    {
                        if (fromState == state || toState == state) continue; // если состояние для удаления в fromState или toState пропускаем их

                        string ToState = transitions.ContainsKey((fromState, state)) ? transitions[(fromState, state)] : null;
                        string Loop = transitions.ContainsKey((state, state)) ? transitions[(state, state)] : null;
                        string FromState = transitions.ContainsKey((state, toState)) ? transitions[(state, toState)] : null;

                        if (ToState != null && FromState != null)
                        {
                            // Если есть путь через удаляемое состояние, обновляем переходы
                            string newTransition = ToState;
                            // добавление в переход замыкания
                            if (Loop != null) newTransition += "(" + Loop + ")*";
                            // котенация перехода и перехода из текущего состояния
                            newTransition += FromState;

                            // Проверка на существование параллельного перехода текущему состоянию
                            var key = (fromState, toState);

                            if (transitions.ContainsKey(key))
                                transitions[key] += "|" + newTransition; // Объединяем с существующим переходом
                            else
                                transitions[key] = newTransition;
                        }
                    }

                // Удаляем все переходы, связанные с удаляемым состоянием
                transitions = transitions
                    .Where(t => t.Key.Item1 != state && t.Key.Item2 != state)
                    .ToDictionary(t => t.Key, t => t.Value);
            }

            // После удаления всех состояний остались только переход между начальным и конечным
            return transitions.ContainsKey((newStart, newFinal)) ? transitions[(newStart, newFinal)] : "";
        }
    }

    public static class RegexSimplifier
    {
        public static string SimplifyRegex(string pattern)
        {
            // Удаление избыточных скобок и пустых альтернатив
            pattern = RemoveEmptyGroups(pattern);

            // Замена сложных выражений с `|` на сокращенные версии
            pattern = SimplifyAlternations(pattern);

            // Удаление избыточных символов в повторяющихся выражениях
            pattern = SimplifyRepetitions(pattern);

            return pattern;
        }

        private static string RemoveEmptyGroups(string pattern)
        {
            // Убираем пустые группы (|) и заменяем ((...)) на (...)
            pattern = Regex.Replace(pattern, @"\(\|*\)", "");
            pattern = Regex.Replace(pattern, @"\(\((.*?)\)\)", "($1)");
            return pattern;
        }

        private static string SimplifyAlternations(string pattern)
        {
            // Упрощаем выражения вида (a|a|a) или (a|b|a) до (a|b)
            pattern = Regex.Replace(pattern, @"\((.)\|(\1)+\)", "$1");
            pattern = Regex.Replace(pattern, @"\((\w)\|(\w)\)", "$1|$2");
            return pattern;
        }

        private static string SimplifyRepetitions(string pattern)
        {
            // Упрощаем повторяющиеся группы, например, ((0|1)*) до (0|1)*
            pattern = Regex.Replace(pattern, @"\(\((.*?)\)\)\*", "($1)*");
            pattern = Regex.Replace(pattern, @"\((.*?)\)\*", "$1*");
            return pattern;
        }
    }
    public static class AutomatonEquivalenceChecker
    {
        // Метод проверки эквивалентности ДКА и НКА
        public static bool AreEquivalent(DFA dfa, NFA nfa, int maxTestLength = 5)
        {
            // Используем алфавит, совпадающий для обоих автоматов
            var sigma = dfa.Sigma;

            // Генерация всех возможных строк до maxTestLength
            foreach (var testString in GenerateTestStrings(sigma, maxTestLength))
            {
                bool dfaAccepts = dfa.Accept(testString);
                bool nfaAccepts = nfa.Accept(testString);

                // Если результаты отличаются, автоматы не эквивалентны
                if (dfaAccepts != nfaAccepts)
                    return false;
            }
            // Если все результаты совпали, то автоматы эквивалентны
            return true;
        }

        // Метод проверки эквивалентности ДКА и регулярного выражения
        public static bool AreEquivalent(DFA dfa, string regexPattern, int maxTestLength = 5)
        {
            Regex regex = new Regex($"^{regexPattern}$");

            // Генерация всех возможных строк до maxTestLength
            foreach (var testString in GenerateTestStrings(dfa.Sigma, maxTestLength))
            {
                bool dfaAccepts = dfa.Accept(testString);
                bool regexAccepts = regex.IsMatch(testString);

                if (dfaAccepts != regexAccepts)
                    return false;
            }
            return true;
        }

        // Метод проверки эквивалентности НКА и регулярного выражения
        public static bool AreEquivalent(NFA nfa, string regexPattern, int maxTestLength = 5)
        {
            Regex regex = new Regex($"^{regexPattern}$");

            foreach (var testString in GenerateTestStrings(nfa.Sigma, maxTestLength))
            {
                bool nfaAccepts = nfa.Accept(testString);
                bool regexAccepts = regex.IsMatch(testString);

                if (nfaAccepts != regexAccepts)
                    return false;
            }
            return true;
        }

        // Метод для генерации всех возможных строк до указанной длины
        private static IEnumerable<string> GenerateTestStrings(string[] sigma, int maxLength)
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(""); // Начинаем с пустой строки

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();

                if (current.Length <= maxLength)
                {
                    yield return current;

                    foreach (string symbol in sigma)
                        queue.Enqueue(current + symbol);
                }
            }
        }
    }
}
