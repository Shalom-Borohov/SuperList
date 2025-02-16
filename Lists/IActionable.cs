﻿using System;

namespace SuperList.Lists
{
    public interface IActionable<T>
    {
        void ApplyByValue(Action<T> doAction, T value);
        void ApplyAll(Action<T> doAction);
        void ApplyByPredicate(Action<T> doAction, Predicate<T> predicate);
    }
}
