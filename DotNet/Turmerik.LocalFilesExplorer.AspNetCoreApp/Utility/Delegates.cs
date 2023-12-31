using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public delegate void ParamsAction(params object[] arguments);

    public delegate void ParamsAction<T1>(T1 arg1, params object[] arguments);

    public delegate void RefAction<T>(ref T t);
    public delegate void RefAction<T, T1>(ref T t, T1 arg1);

    public delegate int IdxRetriever<T, TNmrbl>(TNmrbl nmrbl, int count) where TNmrbl : IEnumerable<T>;

    public delegate bool TryRetrieve1Out<TOutput>(
        out TOutput output);

    public delegate bool TryRetrieve1In1Out<TInput, TOutput>(
        TInput input,
        out TOutput output);

    public delegate bool TryRetrieve2In1Out<TInput1, TInput2, TOutput>(
        TInput1 input1,
        TInput2 input2,
        out TOutput output);

    public delegate bool TryRetrieve1In2Out<TInput, TOutput1, TOutput2>(
        TInput input,
        out TOutput1 output1,
        out TOutput2 output2);

    public delegate TValue UpdateDictnrValue<TKey, TValue>(
        TKey key,
        bool isUpdate,
        TValue value);

    public delegate TOutArr ArraySliceFactory<T, TInArr, TOutArr>(
        TInArr inputArr,
        int startIdx,
        int count);

    public delegate void ForEachCallback<T>(T value, MutableValueWrapper<bool> @break);
    public delegate void ForCallback<T>(T value, int idx, MutableValueWrapper<bool> @break);
    public delegate void ForIdxCallback(int idx, MutableValueWrapper<bool> @break);
}
