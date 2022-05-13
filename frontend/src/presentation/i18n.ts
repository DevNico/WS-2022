import i18n from 'i18next';
import LanguageDetector from 'i18next-browser-languagedetector';
import { initReactI18next } from 'react-i18next';
import translationDE from './i18n/de.json';
import translationEN from './i18n/en.json';

const resources = {
	en: { translation: translationEN },
	de: { translation: translationDE },
};

i18n.use(initReactI18next).use(LanguageDetector).init({
	resources,
	debug: false,
	fallbackLng: navigator.language,
	load: 'languageOnly',
});
