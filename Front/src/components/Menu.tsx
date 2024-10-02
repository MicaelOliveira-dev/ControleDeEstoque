import { useState } from 'react';
import logoutIcon from '../assets/icons-menu/exit.svg'; 
import emailIcon from '../assets/icons-menu/email.svg';
import auditoriaIcon from '../assets/icons-menu/auditoria.svg';
import fornecedoresIcon from '../assets/icons-menu/fornecedores.svg';
import produtosIcon from '../assets/icons-menu/produtos.svg';
import usuariosIcon from '../assets/icons-menu/usuarios.svg';
import relatoriosIcon from '../assets/icons-menu/relatorios.svg';
import LogoIcon from '../assets/icons-menu/logo-icon.svg';
import dashboard from '../assets/icons-menu/dashoard.svg';

const Menu = () => {
  const [showText, setShowText] = useState(true); 
  const [menuWidth, setMenuWidth] = useState('w-80'); 

  const menuItems = [
    { text: 'Dashboard', icon: dashboard },
    { text: 'Produtos', icon: produtosIcon },
    { text: 'Fornecedores', icon: fornecedoresIcon },
    { text: 'Usuários', icon: usuariosIcon },
    { text: 'Alertas', icon: emailIcon },
    { text: 'Relatórios', icon: relatoriosIcon },
    { text: 'Auditoria de movimentação', icon: auditoriaIcon },
  ];

  const toggleMenu = () => {
    setShowText(!showText); 
    setMenuWidth(showText ? 'w-20' : 'w-80'); 
  };

  return (
    <div className={`bg-[#512DA8] text-white h-screen ${menuWidth} p-4 transition-all duration-300`}> 
      <div className="flex items-center mb-10 cursor-pointer" onClick={toggleMenu}>
        <img src={LogoIcon} alt="Logo" className="w-12 h-12 mr-2" />
        {showText && <h1 className="font-['Titillium_Web'] text-2xl font-bold ml-2">Controla aí</h1>}
      </div>
      <ul className="font-['Titillium_Web'] text-[16px] space-y-3">
        {menuItems.map((item) => (
          <li key={item.text} className="flex items-center p-2 rounded-lg hover:bg-[#6136CA] cursor-pointer transition-all duration-300">
            <img src={item.icon} alt={item.text} className="w-8 h-8 mr-3" />
            {showText && <span>{item.text}</span>}
          </li>
        ))}
      </ul>
      <div className="absolute bottom-6 flex items-center p-2 hover:bg-[#6136CA] cursor-pointer rounded-lg transition-all duration-300">
        <img src={logoutIcon} alt="Sair" className="w-8 h-8 mr-3" />
        {showText && <span className='font-["Titillium_Web"] text-[16px]'>Sair</span>}
      </div>
    </div>
  );
};

export default Menu;
