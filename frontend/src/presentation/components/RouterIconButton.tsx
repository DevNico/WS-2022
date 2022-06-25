import Button, { ButtonProps } from '@mui/material/Button/Button';
import IconButton from '@mui/material/IconButton/IconButton';
import React from 'react';
import { Link, LinkProps } from 'react-router-dom';

interface RouterIconButtonProps
	extends Omit<ButtonProps, keyof LinkProps | 'ref'> {
	to: { $: string };
	absolute?: boolean;
	replace?: boolean;
	children?: React.ReactNode;
}

const RouterIconButton: React.FC<RouterIconButtonProps> = ({
	to,
	replace,
	children,
	absolute,
	...rest
}) => {
	let link = to.$;
	if ((absolute ?? true) && !link.startsWith('/')) {
		link = `/${link}`;
	}

	return (
		<IconButton component={Link} to={link} {...rest}>
			{children}
		</IconButton>
	);
};

export default RouterIconButton;
