import type { ReactNode } from 'react';
import { XIcon } from './Icons';

interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  children: ReactNode;
}

export function Modal({ isOpen, onClose, children }: ModalProps) {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center">
      <div 
        className="absolute inset-0 bg-slate-900/50 backdrop-blur-sm animate-fade-in"
        onClick={onClose}
      />
      <div className="relative bg-white rounded-xl shadow-xl max-w-md w-full mx-4 max-h-[90vh] overflow-hidden animate-scale-in">
        <button
          onClick={onClose}
          className="absolute top-4 right-4 p-1 text-slate-400 hover:text-slate-600 transition-colors cursor-pointer"
        >
          <XIcon className="w-5 h-5" />
        </button>
        {children}
      </div>
    </div>
  );
}