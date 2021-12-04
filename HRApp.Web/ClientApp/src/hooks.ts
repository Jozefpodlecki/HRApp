import {
    TypedUseSelectorHook,
    useDispatch as _useDispatch,
    useSelector as _useSelector,
} from "react-redux";
import type { AppDispatch, RootState } from "./store";

export const useDispatch = () => _useDispatch<AppDispatch>();
export const useSelector: TypedUseSelectorHook<RootState> = _useSelector;
