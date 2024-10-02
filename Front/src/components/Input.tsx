import React, { useRef } from 'react';

interface InputProps {
  name: string;
  text: string;
  type: string;
  placeholder?: string;
  value?: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  onIconClick?: () => void;
  icon?: React.ReactNode;
}

const Input: React.FC<InputProps> = ({
  name,
  text,
  type,
  placeholder = '',
  value = '',
  onChange,
  onIconClick,
  icon,
}) => {
  const inputRef = useRef<HTMLInputElement | null>(null);

  return (
    <div>
      <label htmlFor={name} className="block text-sm font-semibold font-['Titillium_Web'] mb-1">
        {text}
      </label>
      <div className="flex mt-1 w-full border border-[#E2E8F0] rounded-md shadow-sm mb-4">
        <input
          ref={inputRef}
          id={name}
          type={type}
          placeholder={placeholder}
          value={value}
          onChange={onChange}
          className={`flex-grow font-['Titillium_Web'] text-black outline-none pr-10 pl-3 py-2`} 
          style={{ paddingRight: '2.5rem' }} 
        />
        {icon && (
          <span
            onClick={onIconClick}
            className="flex items-center cursor-pointer mr-4 ml-4 text-gray-400"
          >
            {icon}
          </span>
        )}
      </div>
    </div>
  );
};

export default Input;
