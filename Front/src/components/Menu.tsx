import logoutIcon from '../assets/icons-menu/exit.svg'; 
import emailIcon from '../assets/icons-menu/email.svg';
import auditoriaIcon from '../assets/icons-menu/auditoria.svg';
import fornecedoresIcon from '../assets/icons-menu/fornecedores.svg';
import produtosIcon from '../assets/icons-menu/produtos.svg';
import usuariosIcon from '../assets/icons-menu/usuarios.svg';
import relatoriosIcon from '../assets/icons-menu/relatorios.svg';
import LogoIcon from '../assets/icons-menu/logo-icon.svg';

const Menu = () => {
  return (
    <div className="bg-[#512DA8] text-white h-screen w-80 p-6">
      <div className="flex items-center mb-20">
        <img src={LogoIcon} alt="Logo" className="w-14 h-14 mr-2" /> 
        <h1 className="font-['Titillium_Web'] text-3xl font-bold ml-[10px] mt-[10px]">Controla aí</h1>
      </div>
      <ul className='font-["Titillium_Web"] text-[20px]'>
        {[
          { text: 'Dashboard', icon: <a><img src={emailIcon} alt="Dashboard" className="w-10 h-10 mr-4" /></a> },
          { text: 'Produtos', icon: <a><img src={produtosIcon} alt="Produtos" className="w-10 h-10 mr-4" /></a> },
          { text: 'Fornecedores', icon: <a><img src={fornecedoresIcon} alt="Fornecedores" className="w-10 h-10 mr-4" /></a> },
          { text: 'Usuários', icon: <a><img src={usuariosIcon} alt="Usuários" className="w-10 h-10 mr-4" /></a> },
          { text: 'Alertas', icon:  <a><img src={emailIcon} alt="Alertas" className="w-10 h-10 mr-4" /></a> },
          { text: 'Relatórios', icon:  <a><img src={relatoriosIcon} alt="Relatórios" className="w-10 h-10 mr-4" /></a> },
          { text: 'Auditoria de movimentação', icon:  <a><img src={auditoriaIcon} alt="Auditoria" className="w-10 h-10 mr-4" /></a> },
        ].map((item) => (
          <li key={item.text} className="flex items-center mb-4 cursor-pointer hover:bg-[#6136CA] p-2 rounded">
            <span>{item.icon}</span>
            <span>{item.text}</span>
          </li>
        ))}
      </ul>
        <div className="mt-[150px] flex items-center cursor-pointer hover:bg-purple-600 p-2 rounded">
            <span className="mr-4">
                <img src={logoutIcon} alt="Sair" className="w-10 h-10" />
            </span>
            <span className='font-["Titillium_Web"] text-[20px]'>Sair</span>
        </div>
    </div>
  );
};

export default Menu;
