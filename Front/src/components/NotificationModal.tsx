import React from 'react';

interface NotificationModalProps {
  isOpen: boolean;
  onClose: () => void;
}

const NotificationModal: React.FC<NotificationModalProps> = ({ isOpen, onClose }) => {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
      <div className="bg-white rounded-lg shadow-lg p-6 max-w-md w-full">
        <div className="flex justify-between items-center mb-4">
          <h2 className="text-xl font-bold">Avisos</h2>
        </div>
        <ul className="space-y-2">
          <li className="bg-gray-100 p-3 rounded-lg">Aviso 1: Mensagem importante sobre o sistema.</li>
          <li className="bg-gray-100 p-3 rounded-lg">Aviso 2: Manutenção agendada para amanhã.</li>
          <li className="bg-gray-100 p-3 rounded-lg">Aviso 3: Nova funcionalidade disponível.</li>
        </ul>
        <div className="mt-4 flex justify-end">
          <button onClick={onClose} className="bg-purple-600 text-white py-2 px-4 rounded-lg hover:bg-purple-700">
            Fechar
          </button>
        </div>
      </div>
    </div>
  );
};

export default NotificationModal;
