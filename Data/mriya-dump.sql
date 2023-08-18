--
-- PostgreSQL database dump
--

-- Dumped from database version 12.9 (Debian 12.9-1.pgdg110+1)
-- Dumped by pg_dump version 12.8

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE IF EXISTS mriya_top;
--
-- Name: mriya_top; Type: DATABASE; Schema: -; Owner: mriya_top
--

CREATE DATABASE mriya_top WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'en_US.utf8' LC_CTYPE = 'en_US.utf8';


ALTER DATABASE mriya_top OWNER TO mriya_top;

\connect mriya_top

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Chat; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Chat" (
    id integer NOT NULL,
    max_users integer DEFAULT 2 NOT NULL,
    name character varying(50),
    startup_id integer,
    date_of_creating timestamp without time zone DEFAULT now() NOT NULL,
    date_last_message timestamp without time zone NOT NULL
);


ALTER TABLE public."Chat" OWNER TO mriya_top;

--
-- Name: Chat_id_seq; Type: SEQUENCE; Schema: public; Owner: mriya_top
--

CREATE SEQUENCE public."Chat_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Chat_id_seq" OWNER TO mriya_top;

--
-- Name: Chat_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: mriya_top
--

ALTER SEQUENCE public."Chat_id_seq" OWNED BY public."Chat".id;


--
-- Name: Language; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Language" (
    id integer NOT NULL,
    language character varying NOT NULL
);


ALTER TABLE public."Language" OWNER TO mriya_top;

--
-- Name: Language_id_seq; Type: SEQUENCE; Schema: public; Owner: mriya_top
--

CREATE SEQUENCE public."Language_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Language_id_seq" OWNER TO mriya_top;

--
-- Name: Language_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: mriya_top
--

ALTER SEQUENCE public."Language_id_seq" OWNED BY public."Language".id;


--
-- Name: Like; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Like" (
    user_id integer NOT NULL,
    post_id integer NOT NULL,
    date timestamp without time zone DEFAULT now() NOT NULL,
    is_active boolean DEFAULT true NOT NULL
);


ALTER TABLE public."Like" OWNER TO mriya_top;

--
-- Name: Message; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Message" (
    id bigint NOT NULL,
    user_id integer NOT NULL,
    chat_id integer NOT NULL,
    text character varying(500) NOT NULL,
    date timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public."Message" OWNER TO mriya_top;

--
-- Name: Message_id_seq; Type: SEQUENCE; Schema: public; Owner: mriya_top
--

CREATE SEQUENCE public."Message_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Message_id_seq" OWNER TO mriya_top;

--
-- Name: Message_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: mriya_top
--

ALTER SEQUENCE public."Message_id_seq" OWNED BY public."Message".id;


--
-- Name: Post; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Post" (
    id integer NOT NULL,
    title character varying(150) NOT NULL,
    text character varying(5000) NOT NULL,
    date_posting timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public."Post" OWNER TO mriya_top;

--
-- Name: Post_id_seq; Type: SEQUENCE; Schema: public; Owner: mriya_top
--

CREATE SEQUENCE public."Post_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Post_id_seq" OWNER TO mriya_top;

--
-- Name: Post_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: mriya_top
--

ALTER SEQUENCE public."Post_id_seq" OWNED BY public."Post".id;


--
-- Name: Post_owner_Startup; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Post_owner_Startup" (
    startup_id integer NOT NULL,
    post_id integer NOT NULL,
    writer_id integer NOT NULL,
    show_writer boolean DEFAULT false NOT NULL
);


ALTER TABLE public."Post_owner_Startup" OWNER TO mriya_top;

--
-- Name: Post_owner_User; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Post_owner_User" (
    user_id integer NOT NULL,
    post_id integer NOT NULL
);


ALTER TABLE public."Post_owner_User" OWNER TO mriya_top;

--
-- Name: Post_standard_Tag; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Post_standard_Tag" (
    post_id integer NOT NULL,
    tag_id integer NOT NULL
);


ALTER TABLE public."Post_standard_Tag" OWNER TO mriya_top;

--
-- Name: Post_stat_Tag; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Post_stat_Tag" (
    post_id integer NOT NULL,
    tag_id integer NOT NULL,
    tag_count smallint
);


ALTER TABLE public."Post_stat_Tag" OWNER TO mriya_top;

--
-- Name: Searching_Specialists; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Searching_Specialists" (
    startup_id integer NOT NULL,
    specialist_id integer NOT NULL
);


ALTER TABLE public."Searching_Specialists" OWNER TO mriya_top;

--
-- Name: Sex; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Sex" (
    id integer NOT NULL,
    sex character varying NOT NULL
);


ALTER TABLE public."Sex" OWNER TO mriya_top;

--
-- Name: Sex_id_seq; Type: SEQUENCE; Schema: public; Owner: mriya_top
--

CREATE SEQUENCE public."Sex_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Sex_id_seq" OWNER TO mriya_top;

--
-- Name: Sex_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: mriya_top
--

ALTER SEQUENCE public."Sex_id_seq" OWNED BY public."Sex".id;


--
-- Name: Specialist; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Specialist" (
    id integer NOT NULL,
    specialist character varying(50) NOT NULL,
    standard boolean DEFAULT false,
    specialist_standard_id integer
);


ALTER TABLE public."Specialist" OWNER TO mriya_top;

--
-- Name: Specialist_id_seq; Type: SEQUENCE; Schema: public; Owner: mriya_top
--

CREATE SEQUENCE public."Specialist_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Specialist_id_seq" OWNER TO mriya_top;

--
-- Name: Specialist_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: mriya_top
--

ALTER SEQUENCE public."Specialist_id_seq" OWNED BY public."Specialist".id;


--
-- Name: Startup; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Startup" (
    id integer NOT NULL,
    login character varying(30) NOT NULL,
    owner integer NOT NULL,
    name character varying(50) NOT NULL,
    short_description character varying(400),
    can_apply boolean DEFAULT true NOT NULL,
    verify boolean DEFAULT false NOT NULL
);


ALTER TABLE public."Startup" OWNER TO mriya_top;

--
-- Name: Startup_Languages; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Startup_Languages" (
    startup_id integer NOT NULL,
    language_id integer DEFAULT 0 NOT NULL
);


ALTER TABLE public."Startup_Languages" OWNER TO mriya_top;

--
-- Name: Startup_employee_User; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Startup_employee_User" (
    user_id integer NOT NULL,
    startup_id integer NOT NULL,
    access_writer boolean DEFAULT false NOT NULL,
    specialist_id integer NOT NULL,
    date_joining timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public."Startup_employee_User" OWNER TO mriya_top;

--
-- Name: Startup_id_seq; Type: SEQUENCE; Schema: public; Owner: mriya_top
--

CREATE SEQUENCE public."Startup_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Startup_id_seq" OWNER TO mriya_top;

--
-- Name: Startup_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: mriya_top
--

ALTER SEQUENCE public."Startup_id_seq" OWNED BY public."Startup".id;


--
-- Name: Startup_standard_Tag; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Startup_standard_Tag" (
    startup_id integer NOT NULL,
    tag_id integer NOT NULL
);


ALTER TABLE public."Startup_standard_Tag" OWNER TO mriya_top;

--
-- Name: Startup_stat_Tag; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Startup_stat_Tag" (
    startup_id integer NOT NULL,
    tag_id integer NOT NULL,
    tag_count smallint NOT NULL
);


ALTER TABLE public."Startup_stat_Tag" OWNER TO mriya_top;

--
-- Name: Subscription_Startup; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Subscription_Startup" (
    user_id integer NOT NULL,
    startup_id integer NOT NULL,
    date timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public."Subscription_Startup" OWNER TO mriya_top;

--
-- Name: Subscription_user; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Subscription_user" (
    user_id integer NOT NULL,
    user_subscribe_id integer NOT NULL,
    date timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public."Subscription_user" OWNER TO mriya_top;

--
-- Name: Tag; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."Tag" (
    id integer NOT NULL,
    tag character varying NOT NULL
);


ALTER TABLE public."Tag" OWNER TO mriya_top;

--
-- Name: Tag_id_seq; Type: SEQUENCE; Schema: public; Owner: mriya_top
--

CREATE SEQUENCE public."Tag_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Tag_id_seq" OWNER TO mriya_top;

--
-- Name: Tag_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: mriya_top
--

ALTER SEQUENCE public."Tag_id_seq" OWNED BY public."Tag".id;


--
-- Name: User; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."User" (
    id integer NOT NULL,
    login character varying(30) NOT NULL,
    email character varying NOT NULL,
    name character varying(50) NOT NULL,
    description character varying(400),
    verify boolean DEFAULT false NOT NULL,
    date_joining timestamp without time zone DEFAULT now() NOT NULL,
    password character varying(256) NOT NULL
);


ALTER TABLE public."User" OWNER TO mriya_top;

--
-- Name: User_Chat; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."User_Chat" (
    chat_id integer NOT NULL,
    user_id integer NOT NULL,
    date_joining integer NOT NULL
);


ALTER TABLE public."User_Chat" OWNER TO mriya_top;

--
-- Name: User_Languages; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."User_Languages" (
    user_id integer NOT NULL,
    language_id integer NOT NULL
);


ALTER TABLE public."User_Languages" OWNER TO mriya_top;

--
-- Name: User_Sex; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."User_Sex" (
    user_id integer NOT NULL,
    sex_id integer DEFAULT 0 NOT NULL
);


ALTER TABLE public."User_Sex" OWNER TO mriya_top;

--
-- Name: User_Specialists; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."User_Specialists" (
    user_id integer NOT NULL,
    specialist_id integer NOT NULL
);


ALTER TABLE public."User_Specialists" OWNER TO mriya_top;

--
-- Name: User_history_Post; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."User_history_Post" (
    user_id integer NOT NULL,
    post_id integer NOT NULL,
    date timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public."User_history_Post" OWNER TO mriya_top;

--
-- Name: User_id_seq; Type: SEQUENCE; Schema: public; Owner: mriya_top
--

CREATE SEQUENCE public."User_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."User_id_seq" OWNER TO mriya_top;

--
-- Name: User_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: mriya_top
--

ALTER SEQUENCE public."User_id_seq" OWNED BY public."User".id;


--
-- Name: User_request_Startup; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."User_request_Startup" (
    user_id integer NOT NULL,
    startup_id integer NOT NULL,
    "position" character varying(50) NOT NULL,
    description character varying(300),
    date timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public."User_request_Startup" OWNER TO mriya_top;

--
-- Name: User_standard_Tag; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."User_standard_Tag" (
    user_id integer NOT NULL,
    tag_id integer NOT NULL
);


ALTER TABLE public."User_standard_Tag" OWNER TO mriya_top;

--
-- Name: User_stat_Tag; Type: TABLE; Schema: public; Owner: mriya_top
--

CREATE TABLE public."User_stat_Tag" (
    user_id integer NOT NULL,
    tag_id integer NOT NULL,
    tag_count smallint NOT NULL
);


ALTER TABLE public."User_stat_Tag" OWNER TO mriya_top;

--
-- Name: User_stat_Tag_count_id_seq; Type: SEQUENCE; Schema: public; Owner: mriya_top
--

CREATE SEQUENCE public."User_stat_Tag_count_id_seq"
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."User_stat_Tag_count_id_seq" OWNER TO mriya_top;

--
-- Name: User_stat_Tag_count_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: mriya_top
--

ALTER SEQUENCE public."User_stat_Tag_count_id_seq" OWNED BY public."User_stat_Tag".tag_count;


--
-- Name: Chat id; Type: DEFAULT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Chat" ALTER COLUMN id SET DEFAULT nextval('public."Chat_id_seq"'::regclass);


--
-- Name: Language id; Type: DEFAULT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Language" ALTER COLUMN id SET DEFAULT nextval('public."Language_id_seq"'::regclass);


--
-- Name: Message id; Type: DEFAULT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Message" ALTER COLUMN id SET DEFAULT nextval('public."Message_id_seq"'::regclass);


--
-- Name: Post id; Type: DEFAULT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Post" ALTER COLUMN id SET DEFAULT nextval('public."Post_id_seq"'::regclass);


--
-- Name: Sex id; Type: DEFAULT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Sex" ALTER COLUMN id SET DEFAULT nextval('public."Sex_id_seq"'::regclass);


--
-- Name: Specialist id; Type: DEFAULT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Specialist" ALTER COLUMN id SET DEFAULT nextval('public."Specialist_id_seq"'::regclass);


--
-- Name: Startup id; Type: DEFAULT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Startup" ALTER COLUMN id SET DEFAULT nextval('public."Startup_id_seq"'::regclass);


--
-- Name: Tag id; Type: DEFAULT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Tag" ALTER COLUMN id SET DEFAULT nextval('public."Tag_id_seq"'::regclass);


--
-- Name: User id; Type: DEFAULT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User" ALTER COLUMN id SET DEFAULT nextval('public."User_id_seq"'::regclass);


--
-- Name: User_stat_Tag tag_count; Type: DEFAULT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_stat_Tag" ALTER COLUMN tag_count SET DEFAULT nextval('public."User_stat_Tag_count_id_seq"'::regclass);


--
-- Name: Chat chat_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Chat"
    ADD CONSTRAINT chat_pk PRIMARY KEY (id);


--
-- Name: Language language_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Language"
    ADD CONSTRAINT language_pk PRIMARY KEY (id);


--
-- Name: Like like_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Like"
    ADD CONSTRAINT like_pk PRIMARY KEY (user_id, post_id);


--
-- Name: Message message_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Message"
    ADD CONSTRAINT message_pk PRIMARY KEY (id);


--
-- Name: Post_owner_Startup post_owner_startup_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Post_owner_Startup"
    ADD CONSTRAINT post_owner_startup_pk PRIMARY KEY (startup_id, post_id);


--
-- Name: Post_owner_User post_owner_user_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Post_owner_User"
    ADD CONSTRAINT post_owner_user_pk PRIMARY KEY (user_id, post_id);


--
-- Name: Post post_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Post"
    ADD CONSTRAINT post_pk PRIMARY KEY (id);


--
-- Name: Post_standard_Tag post_standard_tag_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Post_standard_Tag"
    ADD CONSTRAINT post_standard_tag_pk PRIMARY KEY (post_id, tag_id);


--
-- Name: Post_stat_Tag post_stat_tag_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Post_stat_Tag"
    ADD CONSTRAINT post_stat_tag_pk PRIMARY KEY (post_id, tag_id);


--
-- Name: Searching_Specialists searching_specialists_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Searching_Specialists"
    ADD CONSTRAINT searching_specialists_pk PRIMARY KEY (startup_id, specialist_id);


--
-- Name: Sex sex_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Sex"
    ADD CONSTRAINT sex_pk PRIMARY KEY (id);


--
-- Name: Specialist specialist_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Specialist"
    ADD CONSTRAINT specialist_pk PRIMARY KEY (id);


--
-- Name: Startup_employee_User startup_employee_user_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Startup_employee_User"
    ADD CONSTRAINT startup_employee_user_pk PRIMARY KEY (user_id, startup_id);


--
-- Name: Startup_Languages startup_languages_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Startup_Languages"
    ADD CONSTRAINT startup_languages_pk PRIMARY KEY (startup_id, language_id);


--
-- Name: Startup startup_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Startup"
    ADD CONSTRAINT startup_pk PRIMARY KEY (id);


--
-- Name: Startup_standard_Tag startup_standard_tag_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Startup_standard_Tag"
    ADD CONSTRAINT startup_standard_tag_pk PRIMARY KEY (startup_id, tag_id);


--
-- Name: Startup_stat_Tag startup_stat_tag_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Startup_stat_Tag"
    ADD CONSTRAINT startup_stat_tag_pk PRIMARY KEY (startup_id, tag_id);


--
-- Name: Subscription_Startup subscription_startup_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Subscription_Startup"
    ADD CONSTRAINT subscription_startup_pk PRIMARY KEY (user_id, startup_id);


--
-- Name: Subscription_user subscription_user_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Subscription_user"
    ADD CONSTRAINT subscription_user_pk PRIMARY KEY (user_id, user_subscribe_id);


--
-- Name: Tag tag_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Tag"
    ADD CONSTRAINT tag_pk PRIMARY KEY (id);


--
-- Name: User_Chat user_chat_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_Chat"
    ADD CONSTRAINT user_chat_pk PRIMARY KEY (chat_id, user_id);


--
-- Name: User_history_Post user_history_post_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_history_Post"
    ADD CONSTRAINT user_history_post_pk PRIMARY KEY (user_id, post_id);


--
-- Name: User_Languages user_languages_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_Languages"
    ADD CONSTRAINT user_languages_pk PRIMARY KEY (user_id, language_id);


--
-- Name: User user_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT user_pk PRIMARY KEY (id);


--
-- Name: User_request_Startup user_request_startup_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_request_Startup"
    ADD CONSTRAINT user_request_startup_pk PRIMARY KEY (user_id, startup_id);


--
-- Name: User_Sex user_sex_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_Sex"
    ADD CONSTRAINT user_sex_pk PRIMARY KEY (user_id, sex_id);


--
-- Name: User_Specialists user_specialists_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_Specialists"
    ADD CONSTRAINT user_specialists_pk PRIMARY KEY (user_id, specialist_id);


--
-- Name: User_standard_Tag user_standard_tag_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_standard_Tag"
    ADD CONSTRAINT user_standard_tag_pk PRIMARY KEY (user_id, tag_id);


--
-- Name: User_stat_Tag user_stat_tag_pk; Type: CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_stat_Tag"
    ADD CONSTRAINT user_stat_tag_pk PRIMARY KEY (user_id, tag_id);


--
-- Name: chat_id_uindex; Type: INDEX; Schema: public; Owner: mriya_top
--

CREATE UNIQUE INDEX chat_id_uindex ON public."Chat" USING btree (id);


--
-- Name: language_id_uindex; Type: INDEX; Schema: public; Owner: mriya_top
--

CREATE UNIQUE INDEX language_id_uindex ON public."Language" USING btree (id);


--
-- Name: post_id_uindex; Type: INDEX; Schema: public; Owner: mriya_top
--

CREATE UNIQUE INDEX post_id_uindex ON public."Post" USING btree (id);


--
-- Name: post_owner_startup_post_id_uindex; Type: INDEX; Schema: public; Owner: mriya_top
--

CREATE UNIQUE INDEX post_owner_startup_post_id_uindex ON public."Post_owner_Startup" USING btree (post_id);


--
-- Name: post_owner_user_post_id_uindex; Type: INDEX; Schema: public; Owner: mriya_top
--

CREATE UNIQUE INDEX post_owner_user_post_id_uindex ON public."Post_owner_User" USING btree (post_id);


--
-- Name: sex_id_uindex; Type: INDEX; Schema: public; Owner: mriya_top
--

CREATE UNIQUE INDEX sex_id_uindex ON public."Sex" USING btree (id);


--
-- Name: specialist_id_uindex; Type: INDEX; Schema: public; Owner: mriya_top
--

CREATE UNIQUE INDEX specialist_id_uindex ON public."Specialist" USING btree (id);


--
-- Name: specialist_specialist_uindex; Type: INDEX; Schema: public; Owner: mriya_top
--

CREATE UNIQUE INDEX specialist_specialist_uindex ON public."Specialist" USING btree (specialist);


--
-- Name: startup_id_uindex; Type: INDEX; Schema: public; Owner: mriya_top
--

CREATE UNIQUE INDEX startup_id_uindex ON public."Startup" USING btree (id);


--
-- Name: startup_login_uindex; Type: INDEX; Schema: public; Owner: mriya_top
--

CREATE UNIQUE INDEX startup_login_uindex ON public."Startup" USING btree (login);


--
-- Name: tag_id_uindex; Type: INDEX; Schema: public; Owner: mriya_top
--

CREATE UNIQUE INDEX tag_id_uindex ON public."Tag" USING btree (id);


--
-- Name: tag_tag_uindex; Type: INDEX; Schema: public; Owner: mriya_top
--

CREATE UNIQUE INDEX tag_tag_uindex ON public."Tag" USING btree (tag);


--
-- Name: user_login_uindex; Type: INDEX; Schema: public; Owner: mriya_top
--

CREATE UNIQUE INDEX user_login_uindex ON public."User" USING btree (login);


--
-- Name: user_sex_user_id_uindex; Type: INDEX; Schema: public; Owner: mriya_top
--

CREATE UNIQUE INDEX user_sex_user_id_uindex ON public."User_Sex" USING btree (user_id);


--
-- Name: Like like_post_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Like"
    ADD CONSTRAINT like_post_id_fk FOREIGN KEY (post_id) REFERENCES public."Post"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Like like_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Like"
    ADD CONSTRAINT like_user_id_fk FOREIGN KEY (user_id) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Message message_chat_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Message"
    ADD CONSTRAINT message_chat_id_fk FOREIGN KEY (chat_id) REFERENCES public."Chat"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Message message_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Message"
    ADD CONSTRAINT message_user_id_fk FOREIGN KEY (user_id) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- Name: Post_owner_Startup post_owner_startup_post_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Post_owner_Startup"
    ADD CONSTRAINT post_owner_startup_post_id_fk FOREIGN KEY (post_id) REFERENCES public."Post"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Post_owner_Startup post_owner_startup_startup_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Post_owner_Startup"
    ADD CONSTRAINT post_owner_startup_startup_id_fk FOREIGN KEY (startup_id) REFERENCES public."Startup"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Post_owner_Startup post_owner_startup_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Post_owner_Startup"
    ADD CONSTRAINT post_owner_startup_user_id_fk FOREIGN KEY (writer_id) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- Name: Post_owner_User post_owner_user_post_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Post_owner_User"
    ADD CONSTRAINT post_owner_user_post_id_fk FOREIGN KEY (post_id) REFERENCES public."Post"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Post_owner_User post_owner_user_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Post_owner_User"
    ADD CONSTRAINT post_owner_user_user_id_fk FOREIGN KEY (user_id) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Post_standard_Tag post_standard_tag_post_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Post_standard_Tag"
    ADD CONSTRAINT post_standard_tag_post_id_fk FOREIGN KEY (post_id) REFERENCES public."Post"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Post_standard_Tag post_standard_tag_tag_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Post_standard_Tag"
    ADD CONSTRAINT post_standard_tag_tag_id_fk FOREIGN KEY (tag_id) REFERENCES public."Tag"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Post_stat_Tag post_stat_tag_post_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Post_stat_Tag"
    ADD CONSTRAINT post_stat_tag_post_id_fk FOREIGN KEY (post_id) REFERENCES public."Post"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Post_stat_Tag post_stat_tag_tag_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Post_stat_Tag"
    ADD CONSTRAINT post_stat_tag_tag_id_fk FOREIGN KEY (tag_id) REFERENCES public."Tag"(id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- Name: Searching_Specialists searching_specialists_specialist_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Searching_Specialists"
    ADD CONSTRAINT searching_specialists_specialist_id_fk FOREIGN KEY (specialist_id) REFERENCES public."Specialist"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Searching_Specialists searching_specialists_startup_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Searching_Specialists"
    ADD CONSTRAINT searching_specialists_startup_id_fk FOREIGN KEY (startup_id) REFERENCES public."Startup"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Specialist specialist_specialist_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Specialist"
    ADD CONSTRAINT specialist_specialist_id_fk FOREIGN KEY (specialist_standard_id) REFERENCES public."Specialist"(id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- Name: Startup_employee_User startup_employee_user_specialist_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Startup_employee_User"
    ADD CONSTRAINT startup_employee_user_specialist_id_fk FOREIGN KEY (specialist_id) REFERENCES public."Specialist"(id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- Name: Startup_employee_User startup_employee_user_startup_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Startup_employee_User"
    ADD CONSTRAINT startup_employee_user_startup_id_fk FOREIGN KEY (startup_id) REFERENCES public."Startup"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Startup_employee_User startup_employee_user_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Startup_employee_User"
    ADD CONSTRAINT startup_employee_user_user_id_fk FOREIGN KEY (user_id) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Startup_Languages startup_languages_language_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Startup_Languages"
    ADD CONSTRAINT startup_languages_language_id_fk FOREIGN KEY (language_id) REFERENCES public."Language"(id) ON UPDATE CASCADE ON DELETE SET DEFAULT;


--
-- Name: Startup_Languages startup_languages_startup_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Startup_Languages"
    ADD CONSTRAINT startup_languages_startup_id_fk FOREIGN KEY (startup_id) REFERENCES public."Startup"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Startup_standard_Tag startup_standard_tag_startup_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Startup_standard_Tag"
    ADD CONSTRAINT startup_standard_tag_startup_id_fk FOREIGN KEY (startup_id) REFERENCES public."Startup"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Startup_standard_Tag startup_standard_tag_tag_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Startup_standard_Tag"
    ADD CONSTRAINT startup_standard_tag_tag_id_fk FOREIGN KEY (tag_id) REFERENCES public."Tag"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Startup_stat_Tag startup_stat_tag_startup_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Startup_stat_Tag"
    ADD CONSTRAINT startup_stat_tag_startup_id_fk FOREIGN KEY (startup_id) REFERENCES public."Startup"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Startup_stat_Tag startup_stat_tag_tag_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Startup_stat_Tag"
    ADD CONSTRAINT startup_stat_tag_tag_id_fk FOREIGN KEY (tag_id) REFERENCES public."Tag"(id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- Name: Startup startup_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Startup"
    ADD CONSTRAINT startup_user_id_fk FOREIGN KEY (owner) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Subscription_Startup subscription_startup_startup_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Subscription_Startup"
    ADD CONSTRAINT subscription_startup_startup_id_fk FOREIGN KEY (startup_id) REFERENCES public."Startup"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Subscription_Startup subscription_startup_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Subscription_Startup"
    ADD CONSTRAINT subscription_startup_user_id_fk FOREIGN KEY (user_id) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Subscription_user subscription_user_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Subscription_user"
    ADD CONSTRAINT subscription_user_user_id_fk FOREIGN KEY (user_id) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Subscription_user subscription_user_user_id_fk_2; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."Subscription_user"
    ADD CONSTRAINT subscription_user_user_id_fk_2 FOREIGN KEY (user_subscribe_id) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: User_Chat user_chat_chat_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_Chat"
    ADD CONSTRAINT user_chat_chat_id_fk FOREIGN KEY (chat_id) REFERENCES public."Chat"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: User_Chat user_chat_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_Chat"
    ADD CONSTRAINT user_chat_user_id_fk FOREIGN KEY (user_id) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: User_history_Post user_history_post_post_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_history_Post"
    ADD CONSTRAINT user_history_post_post_id_fk FOREIGN KEY (post_id) REFERENCES public."Post"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: User_history_Post user_history_post_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_history_Post"
    ADD CONSTRAINT user_history_post_user_id_fk FOREIGN KEY (user_id) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: User_Languages user_languages_language_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_Languages"
    ADD CONSTRAINT user_languages_language_id_fk FOREIGN KEY (language_id) REFERENCES public."Language"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: User_Languages user_languages_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_Languages"
    ADD CONSTRAINT user_languages_user_id_fk FOREIGN KEY (user_id) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: User_request_Startup user_request_startup_startup_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_request_Startup"
    ADD CONSTRAINT user_request_startup_startup_id_fk FOREIGN KEY (startup_id) REFERENCES public."Startup"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: User_request_Startup user_request_startup_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_request_Startup"
    ADD CONSTRAINT user_request_startup_user_id_fk FOREIGN KEY (user_id) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: User_Sex user_sex_sex_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_Sex"
    ADD CONSTRAINT user_sex_sex_id_fk FOREIGN KEY (sex_id) REFERENCES public."Sex"(id) ON UPDATE CASCADE ON DELETE SET DEFAULT;


--
-- Name: User_Sex user_sex_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_Sex"
    ADD CONSTRAINT user_sex_user_id_fk FOREIGN KEY (user_id) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: User_Specialists user_specialists_specialist_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_Specialists"
    ADD CONSTRAINT user_specialists_specialist_id_fk FOREIGN KEY (specialist_id) REFERENCES public."Specialist"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: User_Specialists user_specialists_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_Specialists"
    ADD CONSTRAINT user_specialists_user_id_fk FOREIGN KEY (user_id) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: User_standard_Tag user_standard_tag_tag_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_standard_Tag"
    ADD CONSTRAINT user_standard_tag_tag_id_fk FOREIGN KEY (tag_id) REFERENCES public."Tag"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: User_standard_Tag user_standard_tag_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_standard_Tag"
    ADD CONSTRAINT user_standard_tag_user_id_fk FOREIGN KEY (user_id) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: User_stat_Tag user_stat_tag_tag_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_stat_Tag"
    ADD CONSTRAINT user_stat_tag_tag_id_fk FOREIGN KEY (tag_id) REFERENCES public."Tag"(id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- Name: User_stat_Tag user_stat_tag_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: mriya_top
--

ALTER TABLE ONLY public."User_stat_Tag"
    ADD CONSTRAINT user_stat_tag_user_id_fk FOREIGN KEY (user_id) REFERENCES public."User"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

