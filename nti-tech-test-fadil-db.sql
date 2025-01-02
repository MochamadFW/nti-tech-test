/*
 Navicat Premium Data Transfer

 Source Server         : PostgreSQL
 Source Server Type    : PostgreSQL
 Source Server Version : 170002 (170002)
 Source Host           : localhost:5432
 Source Catalog        : nti
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 170002 (170002)
 File Encoding         : 65001

 Date: 02/01/2025 13:10:12
*/


-- ----------------------------
-- Sequence structure for classes_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."classes_id_seq";
CREATE SEQUENCE "public"."classes_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for student_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."student_id_seq";
CREATE SEQUENCE "public"."student_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for subjects_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."subjects_id_seq";
CREATE SEQUENCE "public"."subjects_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for teacher_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."teacher_id_seq";
CREATE SEQUENCE "public"."teacher_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for users_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."users_id_seq";
CREATE SEQUENCE "public"."users_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Table structure for classes
-- ----------------------------
DROP TABLE IF EXISTS "public"."classes";
CREATE TABLE "public"."classes" (
  "id" int4 NOT NULL DEFAULT nextval('classes_id_seq'::regclass),
  "name" varchar(100) COLLATE "pg_catalog"."default" NOT NULL,
  "student_ids" int4[],
  "homeroom_teacher_id" int4,
  "capacity" int4 NOT NULL,
  "status" varchar(10) COLLATE "pg_catalog"."default" NOT NULL
)
;

-- ----------------------------
-- Table structure for student
-- ----------------------------
DROP TABLE IF EXISTS "public"."student";
CREATE TABLE "public"."student" (
  "id" int4 NOT NULL DEFAULT nextval('student_id_seq'::regclass),
  "nisn" varchar(20) COLLATE "pg_catalog"."default" NOT NULL,
  "name" varchar(100) COLLATE "pg_catalog"."default" NOT NULL,
  "grade" int4,
  "gender" varchar(10) COLLATE "pg_catalog"."default" NOT NULL,
  "dob" date NOT NULL,
  "address" text COLLATE "pg_catalog"."default",
  "parents" varchar(50) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for subject
-- ----------------------------
DROP TABLE IF EXISTS "public"."subject";
CREATE TABLE "public"."subject" (
  "id" int4 NOT NULL DEFAULT nextval('subjects_id_seq'::regclass),
  "name" varchar(100) COLLATE "pg_catalog"."default" NOT NULL,
  "type" varchar(20) COLLATE "pg_catalog"."default" NOT NULL,
  "category" varchar(20) COLLATE "pg_catalog"."default" NOT NULL,
  "student_ids" int4[],
  "teacher_ids" int4[],
  "grade_qualification" int4
)
;
COMMENT ON COLUMN "public"."subject"."type" IS 'enum(formal/non-formal)';
COMMENT ON COLUMN "public"."subject"."category" IS 'enum(compulsory/optional)';

-- ----------------------------
-- Table structure for teacher
-- ----------------------------
DROP TABLE IF EXISTS "public"."teacher";
CREATE TABLE "public"."teacher" (
  "id" int4 NOT NULL DEFAULT nextval('teacher_id_seq'::regclass),
  "nip" varchar(18) COLLATE "pg_catalog"."default" NOT NULL,
  "nuptk" varchar(16) COLLATE "pg_catalog"."default" NOT NULL,
  "name" varchar(100) COLLATE "pg_catalog"."default" NOT NULL,
  "gender" varchar(10) COLLATE "pg_catalog"."default" NOT NULL,
  "dob" date NOT NULL,
  "employment_status" varchar(3) COLLATE "pg_catalog"."default" NOT NULL,
  "certification" bool NOT NULL
)
;

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS "public"."users";
CREATE TABLE "public"."users" (
  "id" int4 NOT NULL DEFAULT nextval('users_id_seq'::regclass),
  "name" varchar(100) COLLATE "pg_catalog"."default" NOT NULL,
  "email" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "password" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "role" varchar(10) COLLATE "pg_catalog"."default" NOT NULL
)
;
COMMENT ON COLUMN "public"."users"."role" IS 'enum(admin/user)';

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."classes_id_seq"
OWNED BY "public"."classes"."id";
SELECT setval('"public"."classes_id_seq"', 2, true);

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."student_id_seq"
OWNED BY "public"."student"."id";
SELECT setval('"public"."student_id_seq"', 28, true);

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."subjects_id_seq"
OWNED BY "public"."subject"."id";
SELECT setval('"public"."subjects_id_seq"', 5, true);

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."teacher_id_seq"
OWNED BY "public"."teacher"."id";
SELECT setval('"public"."teacher_id_seq"', 3, true);

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."users_id_seq"
OWNED BY "public"."users"."id";
SELECT setval('"public"."users_id_seq"', 1, true);

-- ----------------------------
-- Checks structure for table classes
-- ----------------------------
ALTER TABLE "public"."classes" ADD CONSTRAINT "classes_status_check" CHECK (status::text = ANY (ARRAY['full'::character varying, 'available'::character varying]::text[]));

-- ----------------------------
-- Primary Key structure for table classes
-- ----------------------------
ALTER TABLE "public"."classes" ADD CONSTRAINT "classes_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Uniques structure for table student
-- ----------------------------
ALTER TABLE "public"."student" ADD CONSTRAINT "student_nisn_key" UNIQUE ("nisn");

-- ----------------------------
-- Checks structure for table student
-- ----------------------------
ALTER TABLE "public"."student" ADD CONSTRAINT "student_gender_check" CHECK (gender::text = ANY (ARRAY['male'::character varying, 'female'::character varying]::text[]));

-- ----------------------------
-- Primary Key structure for table student
-- ----------------------------
ALTER TABLE "public"."student" ADD CONSTRAINT "student_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Checks structure for table subject
-- ----------------------------
ALTER TABLE "public"."subject" ADD CONSTRAINT "subjects_type_check" CHECK (type::text = ANY (ARRAY['formal'::character varying, 'non_formal'::character varying]::text[]));
ALTER TABLE "public"."subject" ADD CONSTRAINT "subjects_category_check" CHECK (category::text = ANY (ARRAY['compulsory'::character varying, 'optional'::character varying]::text[]));

-- ----------------------------
-- Primary Key structure for table subject
-- ----------------------------
ALTER TABLE "public"."subject" ADD CONSTRAINT "subjects_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Uniques structure for table teacher
-- ----------------------------
ALTER TABLE "public"."teacher" ADD CONSTRAINT "teacher_nip_key" UNIQUE ("nip");
ALTER TABLE "public"."teacher" ADD CONSTRAINT "teacher_nuptk_key" UNIQUE ("nuptk");

-- ----------------------------
-- Checks structure for table teacher
-- ----------------------------
ALTER TABLE "public"."teacher" ADD CONSTRAINT "teacher_gender_check" CHECK (gender::text = ANY (ARRAY['male'::character varying, 'female'::character varying]::text[]));
ALTER TABLE "public"."teacher" ADD CONSTRAINT "teacher_employment_status_check" CHECK (employment_status::text = ANY (ARRAY['pns'::character varying, 'gty'::character varying]::text[]));

-- ----------------------------
-- Primary Key structure for table teacher
-- ----------------------------
ALTER TABLE "public"."teacher" ADD CONSTRAINT "teacher_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Uniques structure for table users
-- ----------------------------
ALTER TABLE "public"."users" ADD CONSTRAINT "users_email_key" UNIQUE ("email");

-- ----------------------------
-- Checks structure for table users
-- ----------------------------
ALTER TABLE "public"."users" ADD CONSTRAINT "users_role_check" CHECK (role::text = ANY (ARRAY['admin'::character varying, 'user'::character varying]::text[]));

-- ----------------------------
-- Primary Key structure for table users
-- ----------------------------
ALTER TABLE "public"."users" ADD CONSTRAINT "users_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Foreign Keys structure for table classes
-- ----------------------------
ALTER TABLE "public"."classes" ADD CONSTRAINT "classes_homeroom_teacher_id_fkey" FOREIGN KEY ("homeroom_teacher_id") REFERENCES "public"."teacher" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
