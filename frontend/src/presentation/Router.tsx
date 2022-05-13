import React from 'react';
import { Route, Routes } from 'react-router-dom';
import HomeLayout from './layouts/HomeLayout';
import HomePage from './pages/HomePage';
import NotFoundPage from './pages/NotFoundPage';

const Router: React.FC = () => {
	return (
		<Routes>
			<Route path='/' element={<HomeLayout />}>
				<Route index element={<HomePage />} />
			</Route>
			<Route path='*' element={<NotFoundPage />} />
		</Routes>
	);
};

export default Router;
