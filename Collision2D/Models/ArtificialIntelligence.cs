using Collision2D.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.Utils
{
    public class FSM
    {
        private Stack<FSMState> stateStack = new Stack<FSMState>();
        public delegate void FSMState(FSM fsm, object gameObject);
        public void Update(object gameObject)
        {
            if (stateStack.Peek() != null)
                stateStack.Peek().Invoke(this, gameObject);
        }
        public void pushState(FSMState state)
        {
            stateStack.Push(state);
        }
        public void popState()
        {
            stateStack.Pop();
        }
    }
    public abstract class GoapAction 
    {
        HashSet<KeyValuePair<string, object>> _preconditions;
        HashSet<KeyValuePair<string, object>> _effects;
        private bool _inRange = false;

         /// <summary>
         /// The cost of performing the action.
         /// Figure out a weight that suits the action.
         /// Changing it will affect what actions are chosen during planning.
         /// </summary>
        public float cost = 1f;

        /// <summary>
        /// An action often has to perform on an object. This is that object. Can be null.
        /// </summary>
        public object target;

        public GoapAction()
        {
            _preconditions = new HashSet<KeyValuePair<string, object>>();
            _effects = new HashSet<KeyValuePair<string, object>>();
        }

        public void doReset()
        {
            _inRange = false;
            target = null;
            reset();
        }

        /// <summary>
        /// Reset any variables that need to be reset before planning happens again.
        /// </summary>
        public abstract void reset();

        /// <summary>
        /// Is the action done?
        /// </summary>
        public abstract bool isDone();

        /// <summary>
        /// Procedurally check if this action can run. Not all actions
        /// will need this, but some might.
        /// </summary>
        /// <param name="agent">The GoapAgent that will be interacting with the target</param>
        /// <returns></returns>
        public abstract bool checkProceduralPrecondition(object agent);

        /// <summary>
        /// Run the action.
        /// if something happened and it can no longer perform.In this case
        /// the action queue should clear out and the goal cannot be reached.
        /// </summary>
        /// <param name="agent"></param>
        /// <returns>
        /// Returns True if the action performed successfully or false
        /// </returns>
        public abstract bool perform(object agent);

        /**
         * Does this action need to be within range of a target game object?
         * If not then the moveTo state will not need to run for this action.
         */
        public abstract bool requiresInRange();


        /**
         * Are we in range of the target?
         * The MoveTo state will set this and it gets reset each time this action is performed.
         */
        public bool isInRange()
        {
            return _inRange;
        }

        public void setInRange(bool inRange)
        {
            this._inRange = inRange;
        }


        public void addPrecondition(string key, object value)
        {
            _preconditions.Add(new KeyValuePair<string, object>(key, value));
        }


        public void removePrecondition(string key)
        {
            KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
            foreach (KeyValuePair<string, object> kvp in _preconditions)
            {
                if (kvp.Key.Equals(key))
                    remove = kvp;
            }
            if (!default(KeyValuePair<string, object>).Equals(remove))
                _preconditions.Remove(remove);
        }


        public void addEffect(string key, object value)
        {
            _effects.Add(new KeyValuePair<string, object>(key, value));
        }


        public void removeEffect(string key)
        {
            KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
            foreach (KeyValuePair<string, object> kvp in _effects)
            {
                if (kvp.Key.Equals(key))
                    remove = kvp;
            }
            if (!default(KeyValuePair<string, object>).Equals(remove))
                _effects.Remove(remove);
        }


        public HashSet<KeyValuePair<string, object>> Preconditions
        {
            get
            {
                return _preconditions;
            }
        }

        public HashSet<KeyValuePair<string, object>> Effects
        {
            get
            {
                return _effects;
            }
        }
    }
    public sealed class GoapAgent
    {
        private FSM _stateMachine;

        private FSM.FSMState _idleState;
        private FSM.FSMState _moveToState;
        private FSM.FSMState _performActionState;

        private HashSet<GoapAction> _availableActions;
        private Queue<GoapAction> _currentActions;

        private IGoap _dataProvider;

        private GoapPlanner _planner;

        public bool HasActionPlan { get { return _currentActions.Count > 0; } }

        public GoapAgent(IGoap dataProvider, params GoapAction[] actions)
        {
            _stateMachine = new FSM();
            _availableActions = new HashSet<GoapAction>();
            _currentActions = new Queue<GoapAction>();
            _planner = new GoapPlanner();
            _dataProvider = dataProvider;

            foreach (var a in actions)
                _availableActions.Add(a);

            createIdleState();
            createMoveToState();
            createPerformActionState();
            _stateMachine.pushState(_idleState);
        }
        public void Update()
        {
            _stateMachine.Update(this);
        }
        public void AddAction(GoapAction action)
        {
            _availableActions.Add(action);
        }
        public GoapAction GetAction(Type action)
        {
            return _availableActions.Where(a => a.GetType().Equals(action)).FirstOrDefault();
        }
        public void RemoveAction (GoapAction action)
        {
            _availableActions.Remove(action);
        }
        private void createIdleState()
        {
            _idleState = (fsm, obj) =>
            {
                HashSet<KeyValuePair<string, object>> worldState = _dataProvider.getWorldState();
                HashSet<KeyValuePair<string, object>> goal = _dataProvider.createGoalState();

                Queue<GoapAction> plan = _planner.plan(this, _availableActions, worldState, goal);
                if (plan != null)
                {
                    _currentActions = plan;
                    _dataProvider.planFound(goal, plan);

                    fsm.popState();
                    fsm.pushState(_performActionState);
                }
                else
                {
                    _dataProvider.planFailed(goal);
                    fsm.popState();
                    fsm.pushState(_idleState);
                }
            };
        }
        private void createMoveToState()
        {
            _moveToState = (fsm, obj) =>
            {
                GoapAction action = _currentActions.Peek();
                if (action.requiresInRange() && action.target == null)
                {
                    fsm.popState();
                    fsm.popState();
                    fsm.pushState(_idleState);
                    return;
                }
                if (_dataProvider.moveAgent(action))
                    fsm.popState();
            };
        }
        private void createPerformActionState()
        {
            _performActionState = (fsm, obj) =>
            {
                if (!HasActionPlan)
                {
                    fsm.popState();
                    fsm.pushState(_idleState);
                    _dataProvider.actionsFinished();
                    return;
                }
                GoapAction action = _currentActions.Peek();
                if (action.isDone())
                    _currentActions.Dequeue();
                if (HasActionPlan)
                {
                    action = _currentActions.Peek();
                    bool inRange = action.requiresInRange() ? action.isInRange() : true;
                    if (inRange)
                    {
                        bool success = action.perform(obj);
                        if (!success)
                        {
                            fsm.popState();
                            fsm.pushState(_idleState);
                            _dataProvider.planAborted(action);
                        }
                        else fsm.pushState(_moveToState);
                    }
                }
                else
                {
                    fsm.popState();
                    fsm.pushState(_idleState);
                    _dataProvider.actionsFinished();
                }
            };
        }
    }
    public class GoapPlanner
    {
        public Queue<GoapAction> plan(object agent, HashSet<GoapAction> availableActions, HashSet<KeyValuePair<string, object>> worldState, HashSet<KeyValuePair<string, object>> goal)
        {
            foreach (var a in availableActions)
                a.doReset();
            HashSet<GoapAction> usableActions = new HashSet<GoapAction>();
            foreach (var a in availableActions)
                if (a.checkProceduralPrecondition(agent))
                    usableActions.Add(a);

            List<Node> leaves = new List<Node>();
            Node start = new Node(worldState);
            bool success = buildGraph(start, leaves, usableActions, goal);

            if (!success)
                return null;

            Node cheapest = leaves[0];
            foreach (var leaf in leaves)
                if (leaf._runningCost < cheapest._runningCost)
                    cheapest = leaf;

            List<GoapAction> result = new List<GoapAction>();
            Node n = cheapest;
            while(n != null)
            {
                if (n._action != null)
                    result.Insert(0, n._action);
                n = n._parent;
            }
            Queue<GoapAction> queue = new Queue<GoapAction>();
            foreach (var a in result)
                queue.Enqueue(a);

            return queue;
        }
        private bool buildGraph(Node parent, List<Node> leaves, HashSet<GoapAction> usableActions, HashSet<KeyValuePair<string, object>> goal)
        {
            bool foundOne = false;
            foreach (var action in usableActions)
            {
                if(inState(action.Preconditions, parent._state))
                {
                    HashSet<KeyValuePair<string, object>> currentState = populateState(parent._state, action.Effects);
                    Node node = new Node(parent, parent._runningCost + action.cost, currentState, action);

                    if(inState(goal, currentState))
                    {
                        leaves.Add(node);
                        foundOne = true;
                    }
                    else
                    {
                        HashSet<GoapAction> subset = actionSubset(usableActions, action);
                        bool found = buildGraph(node, leaves, subset, goal);
                        if (found)
                            foundOne = true;
                    }
                }
            }
            return foundOne;
        }
        private HashSet<GoapAction> actionSubset(HashSet<GoapAction> actions, GoapAction removeMe)
        {
            HashSet<GoapAction> subset = new HashSet<GoapAction>();
            foreach (GoapAction a in actions)
            {
                if (!a.Equals(removeMe))
                    subset.Add(a);
            }
            return subset;
        }
        private bool inState (HashSet<KeyValuePair<string,object>> test, HashSet<KeyValuePair<string, object>> state)
        {
            bool allMatch = true;
            foreach (var t in test)
            {
                bool match = false;
                foreach (var s in state)
                    if (s.Equals(t))
                    {
                        match = true;
                        break;
                    }
                if (!match)
                    allMatch = false;
            }
            return allMatch;
        }
        private HashSet<KeyValuePair<string, object>> populateState(HashSet<KeyValuePair<string, object>> currentState, HashSet<KeyValuePair<string, object>> stateChange)
        {
            HashSet<KeyValuePair<string, object>> state = new HashSet<KeyValuePair<string, object>>();
            foreach (KeyValuePair<string, object> s in currentState)
                state.Add(new KeyValuePair<string, object>(s.Key, s.Value));

            foreach (KeyValuePair<string, object> change in stateChange)
            {
                bool exists = false;
                foreach (KeyValuePair<string, object> s in state)
                    if (s.Equals(change))
                    {
                        exists = true;
                        break;
                    }

                if (exists)
                {
                    state.RemoveWhere((KeyValuePair<string, object> kvp) => { return kvp.Key.Equals(change.Key); });
                    KeyValuePair<string, object> updated = new KeyValuePair<string, object>(change.Key, change.Value);
                    state.Add(updated);
                }
                else
                    state.Add(new KeyValuePair<string, object>(change.Key, change.Value));
            }
            return state;
        }

        private class Node
        {
            public Node _parent;
            public float _runningCost;
            public HashSet<KeyValuePair<string, object>> _state;
            public GoapAction _action;

            public Node(Node parent, float runningCost, HashSet<KeyValuePair<string, object>> state, GoapAction action)
            {
                _parent = parent;
                _runningCost = runningCost;
                _state = state;
                _action = action;
            }
            public Node(HashSet<KeyValuePair<string, object>> state)
            {
                _parent = null;
                _runningCost = 0;
                _state = state;
                _action = null;
            }
        }
    }
    public interface IGoap
    {
        HashSet<KeyValuePair<string, object>> getWorldState();
        HashSet<KeyValuePair<string, object>> createGoalState();
        void planFailed(HashSet<KeyValuePair<string, object>> failedGoal);
        void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> acitons);
        void actionsFinished();
        void planAborted(GoapAction aborter);

        /// <summary>
        /// Called during Update. Move the agent towards the target in order
        /// for the next action to be able to perform.
        /// </summary>
        /// <param name="nextAction"></param>
        /// <returns>
        /// Return true if the Agent is at the target and the next action can perform.
        /// False if it is not there yet.
        /// </returns>
        bool moveAgent(GoapAction nextAction);
    }
}

