import type { InputHTMLAttributes } from 'react';

interface InputProps extends InputHTMLAttributes<HTMLInputElement> {
  label?: string;
  error?: string;
}

export function Input({ label, error, className = '', ...props }: InputProps) {
  return (
    <div className="flex flex-col gap-1.5">
      {label && (
        <label className="text-sm font-medium text-slate-700">
          {label}
        </label>
      )}
      <input
        className={`w-full px-4 py-2.5 rounded-lg border transition-all duration-200 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent ${
          error 
            ? 'border-red-500 bg-red-50 text-red-900 placeholder-red-300' 
            : 'border-slate-300 bg-white text-slate-900 placeholder-slate-400 hover:border-slate-400'
        } ${className}`}
        {...props}
      />
      {error && <span className="text-sm text-red-600">{error}</span>}
    </div>
  );
}