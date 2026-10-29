/*
Animal Shelter: 

An animal shelter, which holds only dogs and cats, operates on a strictly "first in, first
out" basis. People must adopt either the "oldest" (based on arrival time) of all animals at the shelter,
or they can select whether they would prefer a dog or a cat (and will receive the oldest animal of
that type). They cannot select which specific animal they would like. Create the data structures to
maintain this system and implement operations such as enqueue, dequeueAny, dequeueDog,
and dequeueCat. You may use the built-in Linked List data structure.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c3q6 {
    public class Solution {
        public abstract class Animal {
            public int Order { get; set; }
            public string Name { get; }

            protected Animal (string name) {
                Name = name;
            }

            public bool IsOlderThan (Animal a) {
                return Order < a.Order;
            }
        }

        public class Dog : Animal {
            public Dog (string name) : base (name) { }
        }

        public class Cat : Animal {
            public Cat (string name) : base (name) { }
        }

        public class AnimalQueue {
            private readonly LinkedList<Dog> _dogs = new LinkedList<Dog> ();
            private readonly LinkedList<Cat> _cats = new LinkedList<Cat> ();
            private int _order = 0;

            public void Enqueue (Animal a) {
                a.Order = _order++;
                if (a is Dog dog) {
                    _dogs.AddLast (dog);
                } else if (a is Cat cat) {
                    _cats.AddLast (cat);
                }
            }

            public Animal DequeueAny () {
                if (_dogs.Count == 0 && _cats.Count == 0) {
                    throw new InvalidOperationException ("Shelter is empty.");
                }
                if (_dogs.Count == 0) return DequeueCat ();
                if (_cats.Count == 0) return DequeueDog ();

                Cat oldestCat = _cats.First.Value;
                Dog oldestDog = _dogs.First.Value;
                if (oldestCat.IsOlderThan (oldestDog)) {
                    return DequeueCat ();
                } else {
                    return DequeueDog ();
                }
            }

            public Dog DequeueDog () {
                if (_dogs.Count == 0) throw new InvalidOperationException ("No dogs available.");
                Dog dog = _dogs.First.Value;
                _dogs.RemoveFirst ();
                return dog;
            }

            public Cat DequeueCat () {
                if (_cats.Count == 0) throw new InvalidOperationException ("No cats available.");
                Cat cat = _cats.First.Value;
                _cats.RemoveFirst ();
                return cat;
            }
        }

        public static void Init (string[] args = null) {
            AnimalQueue shelter = new AnimalQueue ();
            shelter.Enqueue (new Dog ("Rex"));
            shelter.Enqueue (new Cat ("Whiskers"));
            shelter.Enqueue (new Dog ("Buddy"));

            Console.WriteLine ($"Adopted any: {shelter.DequeueAny ().Name}"); // Rex
            Console.WriteLine ($"Adopted cat: {shelter.DequeueCat ().Name}"); // Whiskers
            Console.WriteLine ($"Adopted dog: {shelter.DequeueDog ().Name}"); // Buddy
        }
    }
}
