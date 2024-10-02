import { FC } from "react";
import Input from "./Input"; // Certifique-se de que o caminho está correto

interface AddUserModalProps {
  isOpen: boolean;
  onClose: () => void;
  onAddUser: (user: { name: string; email: string; password: string }) => void;
  newUser: { name: string; email: string; password: string };
  handleInputChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

const CreateNewUserModal: FC<AddUserModalProps> = ({ isOpen, onClose, onAddUser, newUser, handleInputChange }) => {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
      <div className="bg-white rounded-lg p-6 w-96">
        <h2 className="text-lg font-semibold mb-4">Adicionar Usuário</h2>
        <Input
          name="name"
          text="Nome"
          type="text"
          value={newUser.name}
          onChange={handleInputChange}
          placeholder="Digite o nome"
        />
        <Input
          name="email"
          text="Email"
          type="email"
          value={newUser.email}
          onChange={handleInputChange}
          placeholder="Digite o email"
        />
        <Input
          name="password"
          text="Senha"
          type="password"
          value={newUser.password}
          onChange={handleInputChange}
          placeholder="Digite a senha"
        />
        <div className="flex justify-end">
          <button
            onClick={() => onAddUser(newUser)}
            className="bg-purple-600 text-white rounded px-4 py-2 mr-2 transition duration-200 hover:bg-purple-700"
          >
            Salvar
          </button>
          <button
            onClick={onClose}
            className="border border-gray-300 text-gray-700 rounded px-4 py-2 transition duration-200 hover:bg-gray-200"
          >
            Cancelar
          </button>
        </div>
      </div>
    </div>
  );
};

export default CreateNewUserModal;
