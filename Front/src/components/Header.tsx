import { useState } from 'react';
import userIcon from '../assets/icons-menu/user.svg'; 
import alert from '../assets/icons-menu/alert.svg'; 
import NotificationModal from './NotificationModal';

interface HeaderProps {
  pageTitle: string;
  companyName: string;
  userName: string;
  userRole: string;
}

const Header: React.FC<HeaderProps> = ({ pageTitle, companyName, userName, userRole }) => {
  const [isModalOpen, setModalOpen] = useState(false);

  const toggleModal = () => {
    setModalOpen(!isModalOpen);
  };

  return (
    <>
      <header className="flex justify-between items-center bg-[#4A148C] text-white p-4 shadow-md">
        <div className='font-["Titillium_Web"]'>
          <h1 className="text-[30px] font-bold">{pageTitle}</h1>
          <h3 className='font-bold text-[20px]'>{companyName}</h3>
        </div>
        <div className="flex items-center space-x-3 font-['Titillium_Web']">
          <div className="flex space-x-4">
            <div 
              className="relative cursor-pointer hover:bg-purple-600 p-2 rounded"
              onClick={toggleModal}
            >
              <img src={alert} alt="Alertas" className="w-6 h-6 mt-[4px]" />
              <span className="absolute top-0 right-0 bg-red-500 text-white text-xs rounded-full h-4 w-4 flex items-center justify-center mt-[4px]">3</span>
            </div>
          </div>
          <div className="flex items-center cursor-pointer hover:bg-purple-600 rounded">
            <img src={userIcon} alt="User" className="w-8 h-8 mr-2 mt-[4px]" />
            <div className='flex flex-col'>
              <span>{userName}</span>
              <span className='text-[12px]'>{userRole}</span>
            </div>
          </div>
        </div>
      </header>

      <NotificationModal 
        isOpen={isModalOpen} 
        onClose={toggleModal}
      />
    </>
  );
};

export default Header;
