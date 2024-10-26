using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory
{
    /*
    public class DFA
    {
        // Класс для представления состояния
        public class State
        {
            public string Name { get; set; }
            public bool IsAccepting { get; set; }

            public State(string name, bool isAccepting = false)
            {
                Name = name;
                IsAccepting = isAccepting;
            }
        }

        // Класс для представления перехода
        public class Transition
        {
            public char Input { get; set; }
            public State NextState { get; set; }

            public Transition(char input, State nextState)
            {
                Input = input;
                NextState = nextState;
            }
        }

        // Список состояний
        private List<State> states;
        // Список переходов
        private Dictionary<State, List<Transition>> transitions;
        // Начальное состояние
        private State startState;
        // Принимающие состояния
        private List<State> acceptingStates;

        public DFA()
        {
            states = new List<State>();
            transitions = new Dictionary<State, List<Transition>>();
            acceptingStates = new List<State>();
        }

        // Метод для добавления состояния
        public void AddState(State state)
        {
            states.Add(state);
            transitions[state] = new List<Transition>();
            if (state.IsAccepting)
            {
                acceptingStates.Add(state);
            }
        }

        // Метод для добавления перехода
        public void AddTransition(State fromState, char input, State toState)
        {
            if (transitions.ContainsKey(fromState))
            {
                transitions[fromState].Add(new Transition(input, toState));
            }
        }

        // Метод для установки начального состояния
        public void SetStartState(State state)
        {
            startState = state;
        }

        // Метод для проверки, является ли строка допустимой
        public bool IsAccepted(string input)
        {
            State currentState = startState;

            foreach (char c in input)
            {
                bool found = false;
                foreach (Transition transition in transitions[currentState])
                {
                    if (transition.Input == c)
                    {
                        currentState = transition.NextState;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    return false;
                }
            }

            return acceptingStates.Contains(currentState);
        }
    }

    internal class Lab2
    {
        public static void Run()
        {
            // Создание DFA
            DFA dfa = new DFA();

            // Создание состояний
            DFA.State q0 = new DFA.State("q0");
            DFA.State q1 = new DFA.State("q1");
            DFA.State q2 = new DFA.State("q2", true);

            // Добавление состояний в DFA
            dfa.AddState(q0);
            dfa.AddState(q1);
            dfa.AddState(q2);

            // Установка начального состояния
            dfa.SetStartState(q0);

            // Добавление переходов
            dfa.AddTransition(q0, 'a', q1);
            dfa.AddTransition(q1, 'b', q2);

            // Проверка строк
            string input1 = "ab";
            string input2 = "a";
            string input3 = "abc";

            Console.WriteLine($"Input: {input1}, Accepted: {dfa.IsAccepted(input1)}");
            Console.WriteLine($"Input: {input2}, Accepted: {dfa.IsAccepted(input2)}");
            Console.WriteLine($"Input: {input3}, Accepted: {dfa.IsAccepted(input3)}");
        }
    }
    */
}
