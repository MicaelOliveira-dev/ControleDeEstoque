import { useState } from "react";
import { MagnifyingGlassIcon } from "@heroicons/react/24/outline";
import { PencilIcon, UserPlusIcon, ArrowUpCircleIcon, TrashIcon } from "@heroicons/react/24/solid";
import { Tooltip } from "@material-tailwind/react";
import AddUserModal from "../components/CreateNewUserModal";
import EditUserModal from "../components/EditUserModal";
import Header from "../components/Header";
import Menu from "../components/Menu";

const TABLE_HEAD = ["Usuário", "Permissão", "Status", "Último Login", ""];

const TABLE_ROWS = [
    {
        name: "John Michael",
        email: "john@creative-tim.com",
        job: "Ceo",
        rule: "Admin",
        status: true,
        date: "23/04/18 às 18:30",
    },
    {
        name: "Alexa Liras",
        email: "alexa@creative-tim.com",
        job: "Programador",
        rule: "Funcionario",
        status: false,
        date: "23/04/18 às 18:30",
    },
];

const Usuarios = () => {
    const [isAddModalOpen, setIsAddModalOpen] = useState(false);
    const [isEditModalOpen, setIsEditModalOpen] = useState(false);
    const [newUser, setNewUser] = useState({ name: '', email: '', password: '' });
    const [editUser, setEditUser] = useState({ name: '', email: '', password: '' });
    const [searchTerm, setSearchTerm] = useState("");

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        if (isAddModalOpen) {
            setNewUser((prev) => ({ ...prev, [name]: value }));
        } else if (isEditModalOpen) {
            setEditUser((prev) => ({ ...prev, [name]: value }));
        } else {
            setSearchTerm(value);
        }
    };

    const handleAddUser = (user: { name: string; email: string; password: string }) => {
        setIsAddModalOpen(false);
    };

    const handleEditUser = (user: { name: string; email: string; password: string }) => {
        setIsEditModalOpen(false);
    };

    const filteredRows = TABLE_ROWS.filter(row =>
        row.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
        row.email.toLowerCase().includes(searchTerm.toLowerCase())
    );

    return (
        <>
            <div className="bg-[#F3EDFF] w-full min-h-screen flex">
                <Menu />
                <div className="flex-1 flex flex-col">
                    <Header pageTitle={'Usuários'} companyName={"Panificadora Rancho dos Pães"} userName={"Micael Oliveira"} userRole={"Admin"} />
                    <div className="w-[90%] bg-white flex flex-col mt-[50px] ml-[80px] rounded-2xl">
                        <div className="h-full w-full font-['Titillium_Web']">
                            <div className="bg-[#F4F7FC] shadow rounded-lg p-6">
                                <div className="flex flex-col items-center justify-between gap-4 md:flex-row">
                                    <div className="w-full md:w-72">
                                        <div className="relative">
                                            <input
                                                type="text"
                                                className="border border-gray-300 rounded px-4 py-2 w-full focus:outline-none focus:ring-2 focus:ring-purple-500"
                                                placeholder="Pesquisar"
                                                value={searchTerm}
                                                onChange={handleInputChange}
                                            />
                                            <MagnifyingGlassIcon className="absolute right-3 top-2.5 h-5 w-5 text-gray-500" />
                                        </div>
                                    </div>
                                    <div className="flex items-center justify-between">
                                        <div className="flex shrink-0 flex-col gap-3 sm:flex-row">
                                            <button className="flex items-center gap-3 text-white bg-purple-600 rounded px-4 py-2 text-sm transition duration-200 hover:bg-purple-700">
                                                <ArrowUpCircleIcon className="h-4 w-4" /> Exportar Usuários
                                            </button>
                                            <button
                                                onClick={() => setIsAddModalOpen(true)}
                                                className="flex items-center gap-3 text-white bg-purple-600 rounded px-4 py-2 text-sm transition duration-200 hover:bg-purple-700">
                                                <UserPlusIcon className="h-4 w-4" /> Adicionar Usuário
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div className="overflow-hidden mt-4"> {/* Altera para overflow-hidden */}
                                <table className="min-w-full table-auto text-left border-collapse border border-blue-gray-200">
                                    <thead>
                                        <tr>
                                            {TABLE_HEAD.map((head) => (
                                                <th key={head} className="border-b border-blue-gray-100 bg-blue-gray-50 p-4">
                                                    <span className="text-sm font-semibold text-gray-700">{head}</span>
                                                </th>
                                            ))}
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {filteredRows.map(({ name, email, job, rule, status, date }, index) => {
                                            const classes = index === filteredRows.length - 1 ? "p-4" : "p-4 border-b border-blue-gray-50";
                                            return (
                                                <tr key={name} className="hover:bg-gray-100 transition duration-200">
                                                    <td className={classes}>
                                                        <div className="flex items-center gap-3">
                                                            <div className="flex flex-col">
                                                                <span className="text-sm font-normal text-gray-800">{name}</span>
                                                                <span className="text-sm font-normal text-gray-600">{email}</span>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td className={classes}>
                                                        <div className="flex flex-col">
                                                            <span className="text-sm font-normal text-gray-800">{job}</span>
                                                            <span className="text-sm font-normal text-gray-600">{rule}</span>
                                                        </div>
                                                    </td>
                                                    <td className={classes}>
                                                        <div className="w-max">
                                                            <span className={`inline-block px-2 py-1 text-xs font-medium text-white ${status ? 'bg-green-500' : 'bg-gray-400'}`}>
                                                                {status ? "Ativo" : "Inativo"}
                                                            </span>
                                                        </div>
                                                    </td>
                                                    <td className={classes}>
                                                        <span className="text-sm font-normal text-gray-800">{date}</span>
                                                    </td>
                                                    <td className={classes}>
                                                        <div className="flex items-center gap-2">
                                                            <Tooltip title="Editar Usuário">
                                                                <button
                                                                    onClick={() => {
                                                                        setEditUser({ name, email, password: '' });
                                                                        setIsEditModalOpen(true);
                                                                    }}
                                                                    className="text-white bg-purple-600 rounded p-1 transition duration-200 hover:bg-purple-700">
                                                                    <PencilIcon className="h-4 w-4" />
                                                                </button>
                                                            </Tooltip>
                                                            <Tooltip title="Deletar Usuário">
                                                                <button className="text-white bg-red-500 rounded p-1 transition duration-200 hover:bg-red-600">
                                                                    <TrashIcon className="h-4 w-4" />
                                                                </button>
                                                            </Tooltip>
                                                        </div>
                                                    </td>
                                                </tr>
                                            );
                                        })}
                                    </tbody>
                                </table>
                            </div>

                            <div className="flex items-center justify-between border-t border-blue-gray-50 p-4">
                                <span className="text-sm font-normal text-gray-700">Página 1 de 10</span>
                                <div className="flex gap-2">
                                    <button className="border border-purple-500 text-black rounded px-4 py-2 text-sm transition duration-200 hover:bg-purple-500 hover:text-white">Anterior</button>
                                    <button className="border border-purple-500 text-black rounded px-4 py-2 text-sm transition duration-200 hover:bg-purple-500 hover:text-white">Próximo</button>
                                </div>
                            </div>

                            <AddUserModal
                                isOpen={isAddModalOpen}
                                onClose={() => setIsAddModalOpen(false)}
                                onAddUser={handleAddUser}
                                newUser={newUser}
                                handleInputChange={handleInputChange}
                            />
                            <EditUserModal
                                isOpen={isEditModalOpen}
                                onClose={() => setIsEditModalOpen(false)}
                                onEditUser={handleEditUser}
                                user={editUser}
                                handleInputChange={handleInputChange}
                            />
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Usuarios;
