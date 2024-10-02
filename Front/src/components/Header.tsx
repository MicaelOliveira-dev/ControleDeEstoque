import userIcon from '../assets/icons-menu/user.svg'; // Icone do usuário
import relatorioIcon from '../assets/icons-menu/relatorios.svg'; // Ícone do dashboard

const Header = () => {
  return (
    <header className="flex justify-between items-center bg-purple-700 text-white p-4 shadow-md">
      <div className="">
        <h1 className="text-xl font-bold">Dashboard</h1>
        <h3>Nome da Empresa</h3>
      </div>
      <div className="flex items-center space-x-4">
        <div className="flex items-center cursor-pointer hover:bg-purple-600 p-2 rounded">
          <img src={relatorioIcon} alt="Dashboard" className="w-6 h-6 mr-2" />
        </div>
        <div className="flex items-center cursor-pointer hover:bg-purple-600 p-2 rounded">
            <img src={userIcon} alt="User" className="w-6 h-6" />
            <span className="mr-2">Admin</span>          
        </div>
      </div>
    </header>
  );
};

export default Header;
