CREATE DATABASE [TiMi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TiMi', FILENAME = N'F:\Friends\WebApplication\Main\App_Data\TiMi.mdf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TiMi_log', FILENAME = N'F:\Friends\WebApplication\Main\App_Data\TiMi_log.ldf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
 COLLATE Chinese_PRC_CI_AS
GO
ALTER DATABASE [TiMi] SET COMPATIBILITY_LEVEL = 110
GO
ALTER DATABASE [TiMi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TiMi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TiMi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TiMi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TiMi] SET ARITHABORT OFF 
GO
ALTER DATABASE [TiMi] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TiMi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TiMi] SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF)
GO
ALTER DATABASE [TiMi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TiMi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TiMi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TiMi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TiMi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TiMi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TiMi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TiMi] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TiMi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TiMi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TiMi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TiMi] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TiMi] SET  READ_WRITE 
GO
ALTER DATABASE [TiMi] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TiMi] SET  MULTI_USER 
GO
ALTER DATABASE [TiMi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TiMi] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TiMi] SET DELAYED_DURABILITY = DISABLED 
GO
USE [TiMi]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [TiMi]
GO
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY') ALTER DATABASE [TiMi] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO




/*==============================================================*/
/* Table: AUTH                                                  */
/*==============================================================*/
create table AUTH 
(
   AUTH_SESSIONID       NVARCHAR(24)         not null,
   AUTH_USERID          NVARCHAR(10),
   AUTH_LOGINTIME       DATE,
   AUTH_LOGINTYPE       NVARCHAR(1),
   AUTH_CLIENTIP        NVARCHAR(39),
   AUTH_CLIENTNAME      NVARCHAR(50),
   AUTH_DBID            NVARCHAR(10),
   AUTH_LASTREFRESH     DATE,
   AUTH_LASTREQUEST     DATE,
   AUTH_UPDATETIME      DATE,
   AUTH_EXINFO          NVARCHAR(4000),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_AUTH primary key (AUTH_SESSIONID)
);

/*==============================================================*/
/* Table: BBFWPZ                                                */
/*==============================================================*/
create table BBFWPZ 
(
   BBFWPZ_JOBID         NUMERIC               default 0 not null,
   BBFWPZ_BBLX          NVARCHAR(1),
   BBFWPZ_BBLIST        NVARCHAR(250),
   BBFWPZ_XDQH          NUMERIC               default 0 not null,
   BBFWPZ_CXJS          NVARCHAR(1),
   BBFWPZ_SCRID         NVARCHAR(10),
   BBFWPZ_SCR           NVARCHAR(30),
   BBFWPZ_QRRID         NVARCHAR(10),
   BBFWPZ_QRR           NVARCHAR(30),
   BBFWPZ_BYZF          NVARCHAR(60),
   BBFWPZ_BYSZ          NUMERIC               default 0 not null,
   BBFWPZ_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_BBFWPZ primary key (BBFWPZ_JOBID)
);

/*==============================================================*/
/* Table: BBJD                                                  */
/*==============================================================*/
create table BBJD 
(
   BBJD_JDID            NVARCHAR(4)          not null,
   BBJD_JDMC            NVARCHAR(60),
   BBJD_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_BBJD primary key (BBJD_JDID)
);

/*==============================================================*/
/* Table: BBQX                                                  */
/*==============================================================*/
create table BBQX 
(
   BBQX_USERID          NVARCHAR(10)         not null,
   BBQX_JDID            NVARCHAR(4)          not null,
   BBQX_BBBH            NVARCHAR(10)         not null,
   BBQX_SCQX            NVARCHAR(1),
   BBQX_CXQX            NVARCHAR(1),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_BBQX primary key (BBQX_USERID, BBQX_JDID, BBQX_BBBH)
);

/*==============================================================*/
/* Table: BBSC                                                  */
/*==============================================================*/
create table BBSC 
(
   BBSC_BBBH            NVARCHAR(10)         not null,
   BBSC_BBND            NUMERIC               default 0 not null,
   BBSC_BBQH            NUMERIC               default 0 not null,
   BBSC_KSRQ            DATE,
   BBSC_JSRQ            DATE,
   BBSC_BBNR            BINARY,
   BBSC_BZ              NVARCHAR(250),
   BBSC_QR              NVARCHAR(1),
   BBSC_QRRID           NVARCHAR(10),
   BBSC_QRR             NVARCHAR(30),
   BBSC_QRSJ            DATE,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   BBSC_JDID            NVARCHAR(4),
   constraint PK_BBSC primary key (BBSC_BBBH, BBSC_BBND, BBSC_BBQH)
);

/*==============================================================*/
/* Table: BBSZ                                                  */
/*==============================================================*/
create table BBSZ 
(
   BBSZ_BH              NVARCHAR(10)         not null,
   BBSZ_BBMC            NVARCHAR(100),
   BBSZ_BBLX            NVARCHAR(1),
   BBSZ_BBYS            BINARY,
   BBSZ_SM              NVARCHAR(250),
   BBSZ_BBSCR           NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   BBSZ_JDID            NVARCHAR(4),
   constraint PK_BBSZ primary key (BBSZ_BH)
);

/*==============================================================*/
/* Table: BBZDY                                                 */
/*==============================================================*/
create table BBZDY 
(
   BBZDY_ND             NUMERIC               default 0 not null,
   BBZDY_ZKS            NUMERIC               default 0 not null,
   BBZDY_FIRRQ          DATE,
   BBZDY_ZJS            NUMERIC               default 0 not null,
   BBZDY_LASRQ          DATE,
   BBZDY_BZ             NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_BBZDY primary key (BBZDY_ND)
);

/*==============================================================*/
/* Table: BM                                                    */
/*==============================================================*/
create table BM 
(
   BM_BMID              NVARCHAR(10)         not null,
   BM_BMMC              NVARCHAR(30),
   BM_FZRID             NVARCHAR(32),
   BM_DISABLED          NVARCHAR(1),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_BM primary key (BM_BMID)
);

/*==============================================================*/
/* Table: BZ                                                    */
/*==============================================================*/
create table BZ 
(
   BZ_BZBH              NVARCHAR(4)          not null,
   BZ_BZMC              NVARCHAR(40),
   BZ_BMID              NVARCHAR(10),
   BZ_BZID              NVARCHAR(4),
   BZ_ZBID              NVARCHAR(1),
   BZ_BZ                NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   BZ_ZYID              NVARCHAR(4),
   constraint PK_BZ primary key (BZ_BZBH)
);

/*==============================================================*/
/* Table: CODEHELPER                                            */
/*==============================================================*/
create table CODEHELPER 
(
   CODEHELPER_CODEID    NVARCHAR(30)         not null,
   CODEHELPER_CODENAME  NVARCHAR(30),
   CODEHELPER_MDID      NUMERIC               default 0 not null,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_CODEHELPER primary key (CODEHELPER_CODEID)
);

/*==============================================================*/
/* Table: COMPONENT                                             */
/*==============================================================*/
create table COMPONENT 
(
   COMPONENT_COMID      NVARCHAR(12)         not null,
   COMPONENT_COMNAME    NVARCHAR(20),
   COMPONENT_MDIDSTART  NUMERIC               default 0 not null,
   COMPONENT_MDIDEND    NUMERIC               default 0 not null,
   COMPONENT_DISABLED   NVARCHAR(1),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_COMPONENT primary key (COMPONENT_COMID)
);

/*==============================================================*/
/* Table: CZPSZ                                                 */
/*==============================================================*/
create table CZPSZ 
(
   CZPSZ_PZID           NVARCHAR(10)         not null,
   CZPSZ_PZMC           NVARCHAR(40),
   CZPSZ_BMGZ           NVARCHAR(30),
   CZPSZ_BZ             NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_CZPSZ primary key (CZPSZ_PZID)
);

/*==============================================================*/
/* Table: DBSERVER                                              */
/*==============================================================*/
create table DBSERVER 
(
   DBSERVER_DBID        NVARCHAR(10)         not null,
   DBSERVER_DESC        NVARCHAR(40),
   DBSERVER_DBMS        NVARCHAR(10),
   DBSERVER_CONN        NVARCHAR(4000),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_DBSERVER primary key (DBSERVER_DBID)
);

/*==============================================================*/
/* Table: DCJZ                                                  */
/*==============================================================*/
create table DCJZ 
(
   DCJZ_JZID            NVARCHAR(4)          not null,
   DCJZ_JZMC            NVARCHAR(40),
   DCJZ_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_DCJZ primary key (DCJZ_JZID)
);

/*==============================================================*/
/* Table: DCZY                                                  */
/*==============================================================*/
create table DCZY 
(
   DCZY_ZYID            NVARCHAR(4)          not null,
   DCZY_ZYMC            NVARCHAR(40),
   DCZY_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_DCZY primary key (DCZY_ZYID)
);

/*==============================================================*/
/* Table: DEPT                                                  */
/*==============================================================*/
create table DEPT 
(
   DEPT_DEPTID          NVARCHAR(10)         not null,
   DEPT_DEPTNAME        NVARCHAR(30),
   DEPT_FZRID           NVARCHAR(32),
   DEPT_DISABLED        NVARCHAR(1)          default N'N',
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_DEPT primary key (DEPT_DEPTID)
);

/*==============================================================*/
/* Table: DEPTUSER                                              */
/*==============================================================*/
create table DEPTUSER 
(
   DEPTUSER_DEPTID      NVARCHAR(10)         not null,
   DEPTUSER_USERID      NVARCHAR(10)         not null,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_DEPTUSER primary key (DEPTUSER_DEPTID, DEPTUSER_USERID)
);

/*==============================================================*/
/* Table: DFSFILE                                               */
/*==============================================================*/
create table DFSFILE 
(
   DFSFILE_FSID         NVARCHAR(10)         not null,
   DFSFILE_FILEID       NUMERIC               default 0 not null,
   DFSFILE_FILENAME     NVARCHAR(250),
   DFSFILE_EXTNAME      NVARCHAR(10),
   DFSFILE_FILESIZE     NUMERIC               default 0,
   constraint PK_DFSFILE primary key (DFSFILE_FSID, DFSFILE_FILEID)
);

/*==============================================================*/
/* Table: DFSGROUP                                              */
/*==============================================================*/
create table DFSGROUP 
(
   DFSGROUP_GROUPID     NUMERIC               default 0 not null,
   DFSGROUP_FILEID      NUMERIC               default 0 not null,
   DFSFILE_UPLOADTIME   DATE                 default GETDATE(),
   constraint PK_DFSGROUP primary key (DFSGROUP_GROUPID, DFSGROUP_FILEID)
);

/*==============================================================*/
/* Table: DFSSET                                                */
/*==============================================================*/
create table DFSSET 
(
   DFSSET_FSID          NVARCHAR(10)         not null,
   DFSSET_FSNAME        NVARCHAR(30),
   DFSSET_SERVER        NVARCHAR(60),
   DFSSET_PATH          NVARCHAR(100),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_DFSSET primary key (DFSSET_FSID)
);

/*==============================================================*/
/* Table: DQGZFZ                                                */
/*==============================================================*/
create table DQGZFZ 
(
   DQGZFZ_FZID          NVARCHAR(4)          not null,
   DQGZFZ_FZMC          NVARCHAR(60),
   DQGZFZ_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_DQGZFZ primary key (DQGZFZ_FZID)
);

/*==============================================================*/
/* Table: DQGZNR                                                */
/*==============================================================*/
create table DQGZNR 
(
   DQGZNR_GZID          NVARCHAR(4)          not null,
   DQGZNR_FZID          NVARCHAR(4),
   DQGZNR_BZID          NVARCHAR(4),
   DQGZNR_BCID          NVARCHAR(1),
   DQGZNR_GZMC          NVARCHAR(60),
   DQGZNR_SBMC          NVARCHAR(60),
   DQGZNR_GZNR          NVARCHAR(250),
   DQGZNR_ZYSX          NVARCHAR(250),
   DQGZNR_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_DQGZNR primary key (DQGZNR_GZID)
);

/*==============================================================*/
/* Table: DQGZNRMX                                              */
/*==============================================================*/
create table DQGZNRMX 
(
   DQGZNRMX_GZID        NVARCHAR(4)          not null,
   DQGZNRMX_XH          NUMERIC               default 0 not null,
   DQGZNRMX_XMMC        NVARCHAR(100),
   DQGZNRMX_BZ          NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_DQGZNRMX primary key (DQGZNRMX_GZID, DQGZNRMX_XH)
);

/*==============================================================*/
/* Table: DQGZZX                                                */
/*==============================================================*/
create table DQGZZX 
(
   DQGZZX_GZID          NVARCHAR(4)          not null,
   DQGZZX_RQ            DATE                 not null,
   DQGZZX_FZID          NVARCHAR(4),
   DQGZZX_BZID          NVARCHAR(4),
   DQGZZX_BCID          NVARCHAR(1),
   DQGZZX_ZBID          NVARCHAR(1),
   DQGZZX_GZMC          NVARCHAR(60),
   DQGZZX_SBMC          NVARCHAR(60),
   DQGZZX_GZNR          NVARCHAR(250),
   DQGZZX_ZYSX          NVARCHAR(250),
   DQGZZX_GZQK          NVARCHAR(250),
   DQGZZX_KSSJ          DATE,
   DQGZZX_JSSJ          DATE,
   DQGZZX_ZXRID         NVARCHAR(10),
   DQGZZX_ZXR           NVARCHAR(30),
   DQGZZX_XGZXR         NVARCHAR(250),
   DQGZZX_SHRID         NVARCHAR(10),
   DQGZZX_SHR           NVARCHAR(30),
   DQGZZX_SHSJ          DATE,
   DQGZZX_SH            NVARCHAR(1),
   DQGZZX_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_DQGZZX primary key (DQGZZX_GZID, DQGZZX_RQ)
);

/*==============================================================*/
/* Table: DQGZZXMX                                              */
/*==============================================================*/
create table DQGZZXMX 
(
   DQGZZXMX_GZID        NVARCHAR(4)          not null,
   DQGZZXMX_RQ          DATE                 not null,
   DQGZZXMX_XH          NUMERIC               default 0 not null,
   DQGZZXMX_XMMC        NVARCHAR(100),
   DQGZZXMX_ZX          NVARCHAR(1),
   DQGZZXMX_ZXSM        NVARCHAR(250),
   DQGZZXMX_BZ          NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_DQGZZXMX primary key (DQGZZXMX_GZID, DQGZZXMX_RQ, DQGZZXMX_XH)
);

/*==============================================================*/
/* Table: DQGZZXR                                               */
/*==============================================================*/
create table DQGZZXR 
(
   DQGZZXR_GZID         NVARCHAR(4)          not null,
   DQGZZXR_USERID       NVARCHAR(10)         not null,
   DQGZZXR_BZID         NVARCHAR(4),
   DQGZZXR_ZBID         NVARCHAR(1),
   DQGZZXR_XGZXR        NVARCHAR(250),
   DQGZZXR_BZ           NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_DQGZZXR primary key (DQGZZXR_GZID, DQGZZXR_USERID)
);

/*==============================================================*/
/* Table: DQJOB                                                 */
/*==============================================================*/
create table DQJOB 
(
   DQJOB_JOBID          NUMERIC               default 0 not null,
   DQJOB_FZID           NVARCHAR(4),
   DQJOB_GZIDLIST       NVARCHAR(250),
   DQJOB_XDRQ           NUMERIC               default 0 not null,
   DQJOB_SCRID          NVARCHAR(10),
   DQJOB_SCR            NVARCHAR(30),
   DQJOB_BZ             NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_DQJOB primary key (DQJOB_JOBID)
);

/*==============================================================*/
/* Table: DXCZP                                                 */
/*==============================================================*/
create table DXCZP 
(
   DXCZP_DXPID          NVARCHAR(10)         not null,
   DXCZP_DXPMC          NVARCHAR(200),
   DXCZP_PZID           NVARCHAR(10),
   DXCZP_DISABLED       NVARCHAR(1),
   DXCZP_BZ             NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_DXCZP primary key (DXCZP_DXPID)
);

/*==============================================================*/
/* Table: DXCZPWXD                                              */
/*==============================================================*/
create table DXCZPWXD 
(
   DXCZPWXD_DXPID       NVARCHAR(10)         not null,
   DXCZPWXD_XH          NUMERIC               default 0 not null,
   DXCZPWXD_XSXH        NUMERIC               default 0 not null,
   DXCZPWXD_WXD         NVARCHAR(250),
   DXCZPWXD_KZCS        NVARCHAR(250),
   DXCZPWXD_BZ          NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_DXCZPWXD primary key (DXCZPWXD_DXPID, DXCZPWXD_XH)
);

/*==============================================================*/
/* Table: DXCZPXM                                               */
/*==============================================================*/
create table DXCZPXM 
(
   DXCZPXM_DXPID        NVARCHAR(10)         not null,
   DXCZPXM_XH           NUMERIC               default 0 not null,
   DXCZPXM_XSXH         NUMERIC               default 0 not null,
   DXCZPXM_CZXM         NVARCHAR(250),
   DXCZPXM_ZYSX         NVARCHAR(250),
   DXCZPXM_BZ           NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_DXCZPXM primary key (DXCZPXM_DXPID, DXCZPXM_XH)
);

/*==============================================================*/
/* Table: FUNCMODEL                                             */
/*==============================================================*/
create table FUNCMODEL 
(
   FUNCMODEL_CHILDID    NUMERIC               default 0 not null,
   FUNCMODEL_ORDER      NUMERIC               default 0 not null,
   FUNCMODEL_FATHERID   NUMERIC               default 0 not null,
   FUNCMODEL_NAME       NVARCHAR(30)         not null,
   FUNCMODEL_TYPE       NVARCHAR(1)          not null,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_FUNCMODEL primary key (FUNCMODEL_CHILDID)
);

/*==============================================================*/
/* Table: GW                                                    */
/*==============================================================*/
create table GW 
(
   GW_GWID              NVARCHAR(4)          not null,
   GW_GWMC              NVARCHAR(100),
   GW_GWFL              NVARCHAR(2),
   GW_DISABLED          NVARCHAR(1),
   GW_DESC              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_GW primary key (GW_GWID)
);

/*==============================================================*/
/* Table: GZPQXGL                                               */
/*==============================================================*/
create table GZPQXGL 
(
   GZPQXGL_GZPXH        NUMERIC               default 0 not null,
   GZPQXGL_QXBH         NVARCHAR(20)         not null,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_GZPQXGL primary key (GZPQXGL_GZPXH, GZPQXGL_QXBH)
);

/*==============================================================*/
/* Table: GZPSZ                                                 */
/*==============================================================*/
create table GZPSZ 
(
   GZPSZ_PZID           NVARCHAR(10)         not null,
   GZPSZ_PZMC           NVARCHAR(40),
   GZPSZ_PZMD           NUMERIC               default 0 not null,
   GZPSZ_PZYM           NVARCHAR(100),
   GZPSZ_BMGZ           NVARCHAR(30),
   GZPSZ_BZ             NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_GZPSZ primary key (GZPSZ_PZID)
);

/*==============================================================*/
/* Table: GZPSZR                                                */
/*==============================================================*/
create table GZPSZR 
(
   GZPSZR_USERID        NVARCHAR(10)         not null,
   GZPSZR_USERNAME      NVARCHAR(30),
   GZPSZR_SZRLB         NVARCHAR(1)          not null,
   GZPSZR_PZID          NVARCHAR(10)         not null,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_GZPSZR primary key (GZPSZR_USERID, GZPSZR_SZRLB, GZPSZR_PZID)
);

/*==============================================================*/
/* Table: JDLB                                                  */
/*==============================================================*/
create table JDLB 
(
   JDLB_LBID            NVARCHAR(4)          not null,
   JDLB_LBMC            NVARCHAR(40),
   JDLB_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_JDLB primary key (JDLB_LBID)
);

/*==============================================================*/
/* Table: JDSB                                                  */
/*==============================================================*/
create table JDSB 
(
   JDSB_SBBH            NVARCHAR(10)         not null,
   JDSB_SBMC            NVARCHAR(60),
   JDSB_LBID            NVARCHAR(4),
   JDSB_XMID            NVARCHAR(10),
   JDSB_GGXH            NVARCHAR(60),
   JDSB_SCCJ            NVARCHAR(60),
   JDSB_AZBW            NVARCHAR(100),
   JDSB_AZDD            NVARCHAR(100),
   JDSB_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_JDSB primary key (JDSB_SBBH)
);

/*==============================================================*/
/* Table: JDSY                                                  */
/*==============================================================*/
create table JDSY 
(
   JDSY_SYBH            NVARCHAR(10)         not null,
   JDSY_LBID            NVARCHAR(4),
   JDSY_XMID            NVARCHAR(10),
   JDSY_SBBH            NVARCHAR(10),
   JDSY_SYRQ            DATE,
   JDSY_SYDW            NVARCHAR(50),
   JDSY_SYHJ            NVARCHAR(100),
   JDSY_SYR             NVARCHAR(30),
   JDSY_JYR             NVARCHAR(30),
   JDSY_SYY             NVARCHAR(30),
   JDSY_SYJL            NVARCHAR(100),
   JDSY_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_JDSY primary key (JDSY_SYBH)
);

/*==============================================================*/
/* Table: JDSYMX                                                */
/*==============================================================*/
create table JDSYMX 
(
   JDSYMX_SYBH          NVARCHAR(10)         not null,
   JDSYMX_XH            NUMERIC               default 0 not null,
   JDSYMX_MXXMMC        NVARCHAR(100),
   JDSYMX_ZFWS          NVARCHAR(100),
   JDSYMX_ZFWZ          NVARCHAR(100),
   JDSYMX_JLDW          NVARCHAR(40),
   JDSYMX_JSBZ          NVARCHAR(250),
   JDSYMX_SYZ           NVARCHAR(100),
   JDSYMX_SYJL          NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_JDSYMX primary key (JDSYMX_SYBH, JDSYMX_XH)
);

/*==============================================================*/
/* Table: JDXM                                                  */
/*==============================================================*/
create table JDXM 
(
   JDXM_XMID            NVARCHAR(10)         not null,
   JDXM_XMMC            NVARCHAR(60),
   JDXM_LBID            NVARCHAR(4),
   JDXM_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_JDXM primary key (JDXM_XMID)
);

/*==============================================================*/
/* Table: JDXMMX                                                */
/*==============================================================*/
create table JDXMMX 
(
   JDXMMX_XMID          NVARCHAR(10)         not null,
   JDXMMX_XH            NUMERIC               default 0 not null,
   JDXMMX_MXXMMC        NVARCHAR(100),
   JDXMMX_ZFWS          NVARCHAR(100),
   JDXMMX_ZFWZ          NVARCHAR(100),
   JDXMMX_JLDW          NVARCHAR(40),
   JDXMMX_JSBZ          NVARCHAR(250),
   JDXMMX_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_JDXMMX primary key (JDXMMX_XMID, JDXMMX_XH)
);

/*==============================================================*/
/* Table: JOB                                                   */
/*==============================================================*/
create table JOB 
(
   JOB_JOBID            NUMERIC               default 0 not null,
   JOB_JOBNAME          NVARCHAR(40),
   JOB_MDID             NUMERIC               default 0 not null,
   JOB_STATUS           NVARCHAR(1)          default N'C' not null,
   JOB_CYCLEUNIT        NVARCHAR(6)          default N'MINUTE',
   JOB_CYCLEVALUE       NUMERIC               default 0 not null,
   JOB_EXECTIME         NVARCHAR(250),
   JOB_EXECWEEK         NVARCHAR(7),
   JOB_EXECMONTH        NVARCHAR(32),
   JOB_BEGINTIME        DATE,
   JOB_ENDTIME          DATE,
   JOB_EXECUSERID       NVARCHAR(10),
   JOB_FAILNOTICEUSERID NVARCHAR(250),
   JOB_PREEXECTIME      DATE,
   JOB_PREEXECSTATUS    NVARCHAR(1),
   JOB_PREEXECDESC      NVARCHAR(250),
   JOB_NEXTEXECTIME     DATE,
   JOB_STARTTIME        DATE,
   JOB_STOPTIME         DATE,
   JOB_REMARKS          NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_JOB primary key (JOB_JOBID)
);

/*==============================================================*/
/* Table: JOBLOG                                                */
/*==============================================================*/
create table JOBLOG 
(
   JOBLOG_JOBID         NUMERIC               default 0 not null,
   JOBLOG_LOGNO         NUMERIC               default 0 not null,
   JOBLOG_LOGTIME       DATE,
   JOBLOG_OPERTYPE      NVARCHAR(1),
   JOBLOG_EXECRESULT    NVARCHAR(1),
   JOBLOG_DESC          NVARCHAR(250),
   JOBLOG_DETAIL        BINARY,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_JOBLOG primary key (JOBLOG_JOBID, JOBLOG_LOGNO)
);

/*==============================================================*/
/* Table: JXGD                                                  */
/*==============================================================*/
create table JXGD 
(
   JXGD_GDBH            NVARCHAR(10)         not null,
   JXGD_GDMC            NVARCHAR(60),
   JXGD_GZRQ            DATE,
   JXGD_FLID            NVARCHAR(4),
   JXGD_ZRBMID          NVARCHAR(10),
   JXGD_ZRBZBH          NVARCHAR(4),
   JXGD_ZRR             NVARCHAR(30),
   JXGD_SBMC            NVARCHAR(60),
   JXGD_GZXX            NVARCHAR(100),
   JXGD_GZYY            NVARCHAR(100),
   JXGD_GZNR            NVARCHAR(250),
   JXGD_JHKSSJ          DATE,
   JXGD_JHJSSJ          DATE,
   JXGD_JHGS            NUMERIC               default 0 not null,
   JXGD_JHFY            NUMERIC               default 0 not null,
   JXGD_SJKSSJ          DATE,
   JXGD_SJJSSJ          DATE,
   JXGD_SJGS            NUMERIC               default 0 not null,
   JXGD_SJFY            NUMERIC               default 0 not null,
   JXGD_WCQK            NVARCHAR(250),
   JXGD_ZLPJ            NVARCHAR(100),
   JXGD_SH              NVARCHAR(1),
   JXGD_SHR             NVARCHAR(30),
   JXGD_SHRID           NVARCHAR(10),
   JXGD_SHSJ            DATE,
   JXGD_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_JXGD primary key (JXGD_GDBH)
);

/*==============================================================*/
/* Table: JXGDFL                                                */
/*==============================================================*/
create table JXGDFL 
(
   JXGDFL_FLID          NVARCHAR(4)          not null,
   JXGDFL_FLMC          NVARCHAR(40),
   JXGDFL_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_JXGDFL primary key (JXGDFL_FLID)
);

/*==============================================================*/
/* Table: MEPAGE                                                */
/*==============================================================*/
create table MEPAGE 
(
   MEPAGE_MDID          NUMERIC               default 0 not null,
   MEPAGE_WFBID         NVARCHAR(10),
   MEPAGE_COMID         NVARCHAR(12),
   MEPAGE_URL           NVARCHAR(50),
   MEPAGE_TYPE          NVARCHAR(1),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_MEPAGE primary key (MEPAGE_MDID)
);

/*==============================================================*/
/* Table: MODULE                                                */
/*==============================================================*/
create table MODULE 
(
   MODULE_MDID          NUMERIC               default 0 not null,
   MODULE_COMID         NVARCHAR(12),
   MODULE_MDNAME        NVARCHAR(20),
   MODULE_TYPE          NVARCHAR(1),
   MODULE_STDMDID       NUMERIC               default 0 not null,
   MODULE_URL           NVARCHAR(50),
   MODULE_ATTRIBUTE     NVARCHAR(4000),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_MODULE primary key (MODULE_MDID)
);

/*==============================================================*/
/* Table: PARAMETER                                             */
/*==============================================================*/
create table PARAMETER 
(
   PARAMETER_PMID       NVARCHAR(8)          not null,
   PARAMETER_PMMC       NVARCHAR(80),
   PARAMETER_TYPE       NVARCHAR(1),
   PARAMETER_CONTROLTYPE NVARCHAR(1),
   PARAMETER_DESC       NVARCHAR(4000),
   PARAMETER_VALUES     NVARCHAR(4000),
   PARAMETER_DEFAULT    NVARCHAR(80),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(10),
   MODIFIEDTIME         DATE,
   constraint PK_PARAMETER primary key (PARAMETER_PMID)
);

/*==============================================================*/
/* Table: PERMISSION                                            */
/*==============================================================*/
create table PERMISSION 
(
   PERMISSION_TYPE      NVARCHAR(1)          not null,
   PERMISSION_ID        NVARCHAR(10)         not null,
   PERMISSION_AMID      NUMERIC               default 0 not null,
   PERMISSION_INSERT    NVARCHAR(1),
   PERMISSION_EDIT      NVARCHAR(1),
   PERMISSION_DELETE    NVARCHAR(1),
   PERMISSION_PRINT     NVARCHAR(1),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   PERMISSION_VIEW      NVARCHAR(1),
   PERMISSION_DESIGN    NVARCHAR(1),
   constraint PK_PERMISSION primary key (PERMISSION_TYPE, PERMISSION_ID, PERMISSION_AMID)
);

/*==============================================================*/
/* Table: PERMISSIONOP                                          */
/*==============================================================*/
create table PERMISSIONOP 
(
   PERMISSIONOP_XH      NUMERIC               default 0 not null,
   PERMISSIONOP_ID      NVARCHAR(6),
   PERMISSIONOP_NAME    NVARCHAR(20),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_PERMISSIONOP primary key (PERMISSIONOP_XH)
);

/*==============================================================*/
/* Table: PMCONFIG                                              */
/*==============================================================*/
create table PMCONFIG 
(
   PMCONFIG_PMID        NVARCHAR(8)          not null,
   PMCONFIG_MDID        NUMERIC               default 0 not null,
   PMCONFIG_USERID      NVARCHAR(10)         not null,
   PMCONFIG_ROLEID      NVARCHAR(10)         not null,
   PMCONFIG_TYPE        NVARCHAR(1),
   PMCONFIG_VALUE       NVARCHAR(4000),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_PMCONFIG primary key (PMCONFIG_PMID, PMCONFIG_MDID, PMCONFIG_USERID, PMCONFIG_ROLEID)
);

/*==============================================================*/
/* Table: PORTALPANEL                                           */
/*==============================================================*/
create table PORTALPANEL 
(
   PORTALPANEL_USERID   NVARCHAR(10)         not null,
   PORTALPANEL_ROW      NUMERIC               default 0 not null,
   PORTALPANEL_COLUMN   NUMERIC               default 0 not null,
   PORTALPANEL_AMID     NUMERIC               default 0 not null,
   PORTALPANEL_TITLE    NVARCHAR(30),
   PORTALPANEL_TYPE     NVARCHAR(1),
   PORTALPANEL_EXPAND   NVARCHAR(1)          default N'Y',
   PORTALPANEL_WIDTH    NUMERIC               default 0 not null,
   PORTALPANEL_HEIGHT   NUMERIC               default 0 not null,
   PORTALPANEL_RECORDS  NUMERIC               default 0 not null,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_PORTALPANEL primary key (PORTALPANEL_USERID, PORTALPANEL_ROW, PORTALPANEL_COLUMN)
);

/*==============================================================*/
/* Table: QXFL                                                  */
/*==============================================================*/
create table QXFL 
(
   QXFL_FLID            NVARCHAR(4)          not null,
   QXFL_FLMC            NVARCHAR(40),
   QXFL_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_QXFL primary key (QXFL_FLID)
);

/*==============================================================*/
/* Table: QXGL                                                  */
/*==============================================================*/
create table QXGL 
(
   QXGL_QXBH            NVARCHAR(20)         not null,
   QXGL_FLID            NVARCHAR(4),
   QXGL_QXMS            NVARCHAR(100),
   QXGL_QXSB            NVARCHAR(100),
   QXGL_JZMC            NVARCHAR(40),
   QXGL_ZYMC            NVARCHAR(40),
   QXGL_FXBMID          NVARCHAR(10),
   QXGL_FZBZBH          NVARCHAR(4),
   QXGL_FXR             NVARCHAR(30),
   QXGL_FXSJ            DATE,
   QXGL_JXBMID          NVARCHAR(10),
   QXGL_JXBZBH          NVARCHAR(4),
   QXGL_JHWCSJ          DATE,
   QXGL_YSSJ            DATE,
   QXGL_BZ              NVARCHAR(250),
   QXGL_GZPXH           NUMERIC               default 0 not null,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   QXGL_WFID            NVARCHAR(10),
   QXGL_WFRUNID         NUMERIC               default 0 not null,
   QXGL_WFPID           NVARCHAR(4),
   QXGL_WFTODO          NVARCHAR(250),
   QXGL_WFDONE          NVARCHAR(250),
   QXGL_WFPACTDESC      NVARCHAR(20),
   QXGL_JZID            NVARCHAR(4),
   QXGL_ZYID            NVARCHAR(4),
   QXGL_LCXX            NVARCHAR(4000),
   QXGL_SBBM            NVARCHAR(40),
   constraint PK_QXGL primary key (QXGL_QXBH)
);

/*==============================================================*/
/* Table: RCMMZ                                                 */
/*==============================================================*/
create table RCMMZ 
(
   RCMMZ_MZID           NVARCHAR(10)         not null,
   RCMMZ_CYRQ           DATE,
   RCMMZ_KBID           NVARCHAR(10),
   RCMMZ_YSFSID         NVARCHAR(10),
   RCMMZ_ADM            NUMERIC               default 0 not null,
   RCMMZ_ADA            NUMERIC               default 0 not null,
   RCMMZ_ADV            NUMERIC               default 0 not null,
   RCMMZ_ADFC           NUMERIC               default 0 not null,
   RCMMZ_DST            NUMERIC               default 0 not null,
   RCMMZ_MAR            NUMERIC               default 0 not null,
   RCMMZ_AAR            NUMERIC               default 0 not null,
   RCMMZ_VDAF           NUMERIC               default 0 not null,
   RCMMZ_ADH            NUMERIC               default 0 not null,
   RCMMZ_DQGR           NUMERIC               default 0 not null,
   RCMMZ_ARQNET         NUMERIC               default 0 not null,
   RCMMZ_DQGRKCAL       NUMERIC               default 0 not null,
   RCMMZ_ARQNETKCAL     NUMERIC               default 0 not null,
   RCMMZ_T              NUMERIC               default 0 not null,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_RCMMZ primary key (RCMMZ_MZID)
);

/*==============================================================*/
/* Table: REPORTSTYLE                                           */
/*==============================================================*/
create table REPORTSTYLE 
(
   REPORTSTYLE_STYLEID  NVARCHAR(50)         not null,
   REPORTSTYLE_STYLENAME NVARCHAR(40),
   REPORTSTYLE_ORDER    NUMERIC               default 0 not null,
   REPORTSTYLE_DEFAULT  NVARCHAR(1)          default N'N',
   REPORTSTYLE_PUBLIC   NVARCHAR(1)          default N'Y',
   REPORTSTYLE_EXECON   NVARCHAR(250),
   REPORTSTYLE_VERSION  NUMERIC               default 0 not null,
   REPORTSTYLE_STYLE    BINARY,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_REPORTSTYLE primary key (REPORTSTYLE_STYLEID, REPORTSTYLE_ORDER)
);

/*==============================================================*/
/* Table: RLMMZ                                                 */
/*==============================================================*/
create table RLMMZ 
(
   RLMMZ_MZID           NVARCHAR(10)         not null,
   RLMMZ_CYRQ           DATE,
   RLMMZ_BCID           NVARCHAR(1),
   RLMMZ_MAR            NUMERIC               default 0 not null,
   RLMMZ_ADM            NUMERIC               default 0 not null,
   RLMMZ_ADA            NUMERIC               default 0 not null,
   RLMMZ_ADV            NUMERIC               default 0 not null,
   RLMMZ_ADST           NUMERIC               default 0 not null,
   RLMMZ_ADH            NUMERIC               default 0 not null,
   RLMMZ_ADQB           NUMERIC               default 0 not null,
   RLMMZ_DQGR           NUMERIC               default 0 not null,
   RLMMZ_ARQNET         NUMERIC               default 0 not null,
   RLMMZ_AAR            NUMERIC               default 0 not null,
   RLMMZ_VDAF           NUMERIC               default 0 not null,
   RLMMZ_FC             NUMERIC               default 0 not null,
   RLMMZ_T              NUMERIC               default 0 not null,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_RLMMZ primary key (RLMMZ_MZID)
);

/*==============================================================*/
/* Table: RMBZPZ                                                */
/*==============================================================*/
create table RMBZPZ 
(
   RMBZPZ_PZID          NVARCHAR(4)          not null,
   RMBZPZ_BZID          NVARCHAR(4),
   RMBZPZ_DESC          NVARCHAR(250),
   RMBZPZ_DISABLED      NVARCHAR(1)          default N'N',
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_RMBZPZ primary key (RMBZPZ_PZID)
);

/*==============================================================*/
/* Table: RMKB                                                  */
/*==============================================================*/
create table RMKB 
(
   RMKB_KBID            NVARCHAR(10)         not null,
   RMKB_KBNAME          NVARCHAR(30),
   RMKB_DESC            NVARCHAR(250),
   RMKB_DISABLED        NVARCHAR(1)          default N'N',
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_RMKB primary key (RMKB_KBID)
);

/*==============================================================*/
/* Table: RMYSFS                                                */
/*==============================================================*/
create table RMYSFS 
(
   RMYSFS_YSFSID        NVARCHAR(10)         not null,
   RMYSFS_YSFSNAME      NVARCHAR(30),
   RMYSFS_DESC          NVARCHAR(250),
   RMYSFS_DISABLED      NVARCHAR(1)          default N'N',
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_RMYSFS primary key (RMYSFS_YSFSID)
);

/*==============================================================*/
/* Table: ROLE                                                  */
/*==============================================================*/
create table ROLE 
(
   ROLE_ROLEID          NVARCHAR(10)         not null,
   ROLE_ROLENAME        NVARCHAR(30),
   ROLE_DESC            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_ROLE primary key (ROLE_ROLEID)
);

/*==============================================================*/
/* Table: ROLEUSER                                              */
/*==============================================================*/
create table ROLEUSER 
(
   ROLEUSER_ROLEID      NVARCHAR(10)         not null,
   ROLEUSER_USERID      NVARCHAR(10)         not null,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_ROLEUSER primary key (ROLEUSER_ROLEID, ROLEUSER_USERID)
);

/*==============================================================*/
/* Table: RTANAINDEX                                            */
/*==============================================================*/
create table RTANAINDEX 
(
   RTANAINDEX_TAGNAME   NVARCHAR(60)         not null,
   RTANAINDEX_TAGINDEX  NUMERIC               default 0,
   RTANAINDEX_TAGDESC   NVARCHAR(80),
   RTANAINDEX_DESCC     NVARCHAR(80),
   RTANAINDEX_TAGTYPE   NVARCHAR(1),
   RTANAINDEX_EU        NVARCHAR(6),
   RTANAINDEX_MIN       NUMERIC               default 0,
   RTANAINDEX_MAX       NUMERIC               default 0,
   RTANAINDEX_EMIN      NUMERIC               default 0,
   RTANAINDEX_EMAX      NUMERIC               default 0,
   RTANAINDEX_TIMES     NUMERIC               default 0,
   RTANAINDEX_OFFSET    NUMERIC               default 0,
   RTANAINDEX_LO        NUMERIC               default 0,
   RTANAINDEX_HI        NUMERIC               default 0,
   RTANAINDEX_LOEN      NVARCHAR(1),
   RTANAINDEX_HIEN      NVARCHAR(1),
   RTANAINDEX_DELTA     NUMERIC               default 0,
   RTANAINDEX_ACCESS    NVARCHAR(1),
   RTANAINDEX_CYCTYPE   NUMERIC               default 0,
   RTANAINDEX_DECIMAL   NVARCHAR(1),
   RTANAINDEX_REMARK    NVARCHAR(250),
   RTANAINDEX_LOOP      NUMERIC               default 0,
   RTANAINDEX_PCU       NUMERIC               default 0,
   RTANAINDEX_MODULE    NUMERIC               default 0,
   RTANAINDEX_BLOCK     NUMERIC               default 0,
   RTANAINDEX_EXCDEV    NUMERIC               default 0,
   RTANAINDEX_EXCMIN    NUMERIC               default 0,
   RTANAINDEX_EXCMAX    NUMERIC               default 0,
   RTANAINDEX_COMPST    NVARCHAR(1),
   RTANAINDEX_COMPDEV   NUMERIC               default 0,
   RTANAINDEX_COMPMIN   NUMERIC               default 0,
   RTANAINDEX_COMPMAX   NUMERIC               default 0,
   RTANAINDEX_HOURSAV   NVARCHAR(1),
   RTANAINDEX_HALFSAV   NVARCHAR(1),
   RTANAINDEX_WHSJ      DATE,
   RTANAINDEX_DATATYPE  NVARCHAR(6),
   RTANAINDEX_DATASRC   NVARCHAR(10),
   RTANAINDEX_DATAADDR  NVARCHAR(250),
   RTANAINDEX_RSID      NVARCHAR(6),
   RTANAINDEX_QYRQ      DATE,
   RTANAINDEX_TYRQ      DATE,
   RTANAINDEX_TYBZ      NVARCHAR(1),
   RTANAINDEX_WHR       NVARCHAR(30),
   RTANAINDEX_WHRID     NVARCHAR(10),
   RTANAINDEX_TABLEINDEX NUMERIC               default 0,
   RTANAINDEX_FIELDINDEX NUMERIC               default 0,
   RTANAINDEX_TABLENAME NVARCHAR(20),
   RTANAINDEX_FIELDNAME NVARCHAR(20),
   RTANAINDEX_STORAGETYPE NVARCHAR(4),
   RTANAINDEX_SBBM      NVARCHAR(60),
   RTANAINDEX_UNIT      NVARCHAR(2),
   TDSC                 NVARCHAR(80),
   constraint PK_RTANAINDEX primary key (RTANAINDEX_TAGNAME)
);

/*==============================================================*/
/* Table: RTUPDATE                                              */
/*==============================================================*/
create table RTUPDATE 
(
   RTUPDATE_ID          NVARCHAR(20)         not null,
   RTUPDATE_DATETIME    DATE,
   RTUPDATE_XYGX        NVARCHAR(1)          default N'N',
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_RTUPDATE primary key (RTUPDATE_ID)
);

/*==============================================================*/
/* Table: SBBM                                                  */
/*==============================================================*/
create table SBBM 
(
   SBBM_SBXH            NUMERIC               default 0 not null,
   SBBM_SBBM            NVARCHAR(40),
   SBBM_FSBXH           NUMERIC               default 0 not null,
   SBBM_SBMC            NVARCHAR(100),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   SBBM_JZID            NVARCHAR(4),
   SBBM_ZRGW            NVARCHAR(4),
   constraint PK_SBBM primary key (SBBM_SBXH)
);

/*==============================================================*/
/* Table: SSCDB                                                 */
/*==============================================================*/
create table SSCDB 
(
   SSCDB_TAGNAME        NVARCHAR(60)         not null,
   SSCDB_GID            NUMERIC               default 0 not null,
   SSCDB_TAGBQMC        NVARCHAR(100),
   SSCDB_TYPE           NVARCHAR(1),
   SSCDB_JLDW           NVARCHAR(40),
   SSCDB_TABLEINDEX     NUMERIC               default 0 not null,
   SSCDB_FIELDINDEX     NUMERIC               default 0 not null,
   SSCDB_DSOUID         NVARCHAR(4),
   SSCDB_DSOUMC         NVARCHAR(100),
   SSCDB_DECI           NUMERIC               default 0 not null,
   SSCDB_MAX            NUMERIC               default 0 not null,
   SSCDB_MIN            NUMERIC               default 0 not null,
   SSCDB_EMAX           NUMERIC               default 0 not null,
   SSCDB_EMIN           NUMERIC               default 0 not null,
   SSCDB_BZ             NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   SSCDB_DCSTAGNAME     NVARCHAR(60),
   constraint PK_SSCDB primary key (SSCDB_TAGNAME)
);

/*==============================================================*/
/* Table: SSCDBBF                                               */
/*==============================================================*/
create table SSCDBBF 
(
   SSCDB_TAGNAME        NVARCHAR(60)         not null,
   SSCDB_GID            NUMERIC               not null,
   SSCDB_TAGBQMC        NVARCHAR(100),
   SSCDB_TYPE           NVARCHAR(1),
   SSCDB_JLDW           NVARCHAR(40),
   SSCDB_TABLEINDEX     NUMERIC               not null,
   SSCDB_FIELDINDEX     NUMERIC               not null,
   SSCDB_DSOUID         NVARCHAR(4),
   SSCDB_DSOUMC         NVARCHAR(100),
   SSCDB_DECI           NUMERIC               not null,
   SSCDB_MAX            NUMERIC               not null,
   SSCDB_MIN            NUMERIC               not null,
   SSCDB_EMAX           NUMERIC               not null,
   SSCDB_EMIN           NUMERIC               not null,
   SSCDB_BZ             NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   SSCDB_DCSTAGNAME     NVARCHAR(60)
);

/*==============================================================*/
/* Table: SSFWPZ                                                */
/*==============================================================*/
create table SSFWPZ 
(
   SSFWPZ_PZID          NVARCHAR(10)         not null,
   SSFWPZ_WEBIP         NVARCHAR(40),
   SSFWPZ_WEBPORT       NUMERIC               default 0 not null,
   SSFWPZ_CYCLE         NUMERIC               default 0 not null,
   SSFWPZ_BYZF          NVARCHAR(60),
   SSFWPZ_BYSZ          NUMERIC               default 0 not null,
   SSFWPZ_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_SSFWPZ primary key (SSFWPZ_PZID)
);

/*==============================================================*/
/* Table: SSZTJD                                                */
/*==============================================================*/
create table SSZTJD 
(
   SSZTJD_JDID          NVARCHAR(4)          not null,
   SSZTJD_JDMC          NVARCHAR(60),
   SSZTJD_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_SSZTJD primary key (SSZTJD_JDID)
);

/*==============================================================*/
/* Table: SSZTT                                                 */
/*==============================================================*/
create table SSZTT 
(
   SSZTT_ZTXH           NUMERIC               default 0 not null,
   SSZTT_JDID           NVARCHAR(4),
   SSZTT_ZTMC           NVARCHAR(40),
   SSZTT_XSSX           NUMERIC               default 0 not null,
   SSZTT_ZTT            BINARY,
   SSZTT_SXZQ           NUMERIC               default 0 not null,
   SSZTT_BZ             NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_SSZTT primary key (SSZTT_ZTXH)
);

/*==============================================================*/
/* Table: SYSTEM                                                */
/*==============================================================*/
create table SYSTEM 
(
   SYSTEM_ID            NVARCHAR(10)         not null,
   SYSTEM_NAME          NVARCHAR(30),
   SYSTEM_PSWLENGTH     NUMERIC               default 0 not null,
   SYSTEM_PSWDAYS       NUMERIC               default 0 not null,
   SYSTEM_PSWWARNDAYS   NUMERIC               default 0 not null,
   SYSTEM_PSWNEW        NVARCHAR(1),
   SYSTEM_PSWHISTORYCOUNT NUMERIC               default 0 not null,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   SYSTEM_LIMITEDDATE   DATE,
   SYSTEM_MACHINEID NVARCHAR(20),
   SYSTEM_KEY NVARCHAR(200),
   constraint PK_SYSTEM primary key (SYSTEM_ID)
);

/*==============================================================*/
/* Table: TJBM                                                  */
/*==============================================================*/
create table TJBM 
(
   TJBM_ZBFZID          NVARCHAR(4)          not null,
   TJBM_RQ              DATE                 not null,
   TJBM_SJ              NVARCHAR(5)          not null,
   TJBM_RQSJ            DATE,
   TJBM_QR              NVARCHAR(1),
   TJBM_QRRID           NVARCHAR(10),
   TJBM_QRR             NVARCHAR(30),
   TJBM_QRSJ            DATE,
   TJBM_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_TJBM primary key (TJBM_ZBFZID, TJBM_RQ, TJBM_SJ)
);

/*==============================================================*/
/* Table: TJBMSS                                                */
/*==============================================================*/
create table TJBMSS 
(
   TJBMSS_ZBFZID        NVARCHAR(4)          not null,
   TJBMSS_RQ            DATE                 not null,
   TJBMSS_SJ            NVARCHAR(5)          not null,
   TJBMSS_ZBBM          NUMERIC               default 0 not null,
   TJBMSS_RQSJ          DATE,
   TJBMSS_SQZ           NUMERIC               default 0 not null,
   TJBMSS_ZBZ           NUMERIC               default 0 not null,
   TJBMSS_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   TJBMSS_BL            NUMERIC               default 0 not null,
   constraint PK_TJBMSS primary key (TJBMSS_ZBFZID, TJBMSS_RQ, TJBMSS_SJ, TJBMSS_ZBBM)
);

/*==============================================================*/
/* Table: TJHBBM                                                */
/*==============================================================*/
create table TJHBBM 
(
   TJHBBM_RQSJ          DATE                 not null,
   TJHBBM_ZBBM          NUMERIC               default 0 not null,
   TJHBBM_HQBL          NUMERIC               default 0 not null,
   TJHBBM_HQSS          NUMERIC               default 0 not null,
   TJHBBM_HHBL          NUMERIC               default 0 not null,
   TJHBBM_HHSS          NUMERIC               default 0 not null,
   TJHBBM_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_TJHBBM primary key (TJHBBM_RQSJ, TJHBBM_ZBBM)
);

/*==============================================================*/
/* Table: TJJOB                                                 */
/*==============================================================*/
create table TJJOB 
(
   TJJOB_JOBID          NUMERIC               default 0 not null,
   TJJOB_ZBLX           NVARCHAR(1),
   TJJOB_BZID           NVARCHAR(4),
   TJJOB_BCID           NVARCHAR(1),
   TJJOB_FZLIST         NVARCHAR(250),
   TJJOB_SJ             NVARCHAR(5),
   TJJOB_XDRQ           NUMERIC               default 0 not null,
   TJJOB_SCRID          NVARCHAR(10),
   TJJOB_SCR            NVARCHAR(30),
   TJJOB_QRRID          NVARCHAR(10),
   TJJOB_QRR            NVARCHAR(30),
   TJJOB_BYZF           NVARCHAR(60),
   TJJOB_BYSZ           NUMERIC               default 0 not null,
   TJJOB_BZ             NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_TJJOB primary key (TJJOB_JOBID)
);

/*==============================================================*/
/* Table: TJRZB                                                 */
/*==============================================================*/
create table TJRZB 
(
   TJRZB_RQ             DATE                 not null,
   TJRZB_ZBFZID         NVARCHAR(4)          not null,
   TJRZB_QR             NVARCHAR(1),
   TJRZB_QRRID          NVARCHAR(10),
   TJRZB_QRR            NVARCHAR(30),
   TJRZB_QRSJ           DATE,
   TJRZB_BZ             NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_TJRZB primary key (TJRZB_RQ, TJRZB_ZBFZID)
);

/*==============================================================*/
/* Table: TJRZBZ                                                */
/*==============================================================*/
create table TJRZBZ 
(
   TJRZBZ_RQ            DATE                 not null,
   TJRZBZ_ZBFZID        NVARCHAR(4)          not null,
   TJRZBZ_ZBBM          NUMERIC               default 0 not null,
   TJRZBZ_SQZ           NUMERIC               default 0 not null,
   TJRZBZ_ZBZ           NUMERIC               default 0 not null,
   TJRZBZ_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_TJRZBZ primary key (TJRZBZ_RQ, TJRZBZ_ZBFZID, TJRZBZ_ZBBM)
);

/*==============================================================*/
/* Table: TJSBFZ                                                */
/*==============================================================*/
create table TJSBFZ 
(
   TJSBFZ_FZID          NVARCHAR(4)          not null,
   TJSBFZ_FZMC          NVARCHAR(40),
   TJSBFZ_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   TJSBFZ_JZID          NVARCHAR(4),
   constraint PK_TJSBFZ primary key (TJSBFZ_FZID)
);

/*==============================================================*/
/* Table: TJYZB                                                 */
/*==============================================================*/
create table TJYZB 
(
   TJYZB_YEAR           NUMERIC               default 0 not null,
   TJYZB_MONTH          NUMERIC               default 0 not null,
   TJYZB_ZBFZID         NVARCHAR(4)          not null,
   TJYZB_QR             NVARCHAR(1),
   TJYZB_QRRID          NVARCHAR(10),
   TJYZB_QRR            NVARCHAR(30),
   TJYZB_QRSJ           DATE,
   TJYZB_BZ             NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_TJYZB primary key (TJYZB_YEAR, TJYZB_MONTH, TJYZB_ZBFZID)
);

/*==============================================================*/
/* Table: TJYZBZ                                                */
/*==============================================================*/
create table TJYZBZ 
(
   TJYZBZ_YEAR          NUMERIC               default 0 not null,
   TJYZBZ_MONTH         NUMERIC               default 0 not null,
   TJYZBZ_ZBFZID        NVARCHAR(4)          not null,
   TJYZBZ_ZBBM          NUMERIC               default 0 not null,
   TJYZBZ_SQZ           NUMERIC               default 0,
   TJYZBZ_ZBZ           NUMERIC               default 0,
   TJYZBZ_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   U_TJYZBZ_ZBMC        NVARCHAR(100),
   U_TJYZBZ_XSSX        FLOAT,
   constraint PK_TJYZBZ primary key (TJYZBZ_YEAR, TJYZBZ_MONTH, TJYZBZ_ZBFZID, TJYZBZ_ZBBM)
);

/*==============================================================*/
/* Table: TJZB                                                  */
/*==============================================================*/
create table TJZB 
(
   TJZB_ZBBM            NUMERIC               default 0 not null,
   TJZB_ZBMC            NVARCHAR(40),
   TJZB_TAGNAME         NVARCHAR(60),
   TJZB_XSWS            NUMERIC               default 0 not null,
   TJZB_JLDW            NVARCHAR(40),
   TJZB_SBFZID          NVARCHAR(4),
   TJZB_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_TJZB primary key (TJZB_ZBBM)
);

/*==============================================================*/
/* Table: TJZBFA                                                */
/*==============================================================*/
create table TJZBFA 
(
   TJZBFA_FAXH          NUMERIC               default 0 not null,
   TJZBFA_FAMC          NVARCHAR(40),
   TJZBFA_ZBLX          NVARCHAR(1),
   TJZBFA_USERID        NVARCHAR(10),
   TJZBFA_FZID          NVARCHAR(4),
   TJZBFA_ZBLIST        NVARCHAR(250),
   TJZBFA_SXH           NUMERIC               default 0 not null,
   TJZBFA_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_TJZBFA primary key (TJZBFA_FAXH)
);

/*==============================================================*/
/* Table: TJZBFZ                                                */
/*==============================================================*/
create table TJZBFZ 
(
   TJZBFZ_ZBLX          NVARCHAR(1)          not null,
   TJZBFZ_FZID          NVARCHAR(4)          not null,
   TJZBFZ_FZMC          NVARCHAR(40),
   TJZBFZ_BZID          NVARCHAR(4),
   TJZBFZ_BMSJ          NVARCHAR(250),
   TJZBFZ_LRRLIST       NVARCHAR(250),
   TJZBFZ_QRRLIST       NVARCHAR(250),
   TJZBFZ_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_TJZBFZ primary key (TJZBFZ_ZBLX, TJZBFZ_FZID)
);

/*==============================================================*/
/* Table: TJZBGS                                                */
/*==============================================================*/
create table TJZBGS 
(
   TJZBGS_ZBLX          NVARCHAR(1)          not null,
   TJZBGS_ZBBM          NUMERIC               default 0 not null,
   TJZBGS_ZBMC          NVARCHAR(120),
   TJZBGS_ZBFZID        NVARCHAR(4),
   TJZBGS_JLDW          NVARCHAR(40),
   TJZBGS_XSWS          NUMERIC               default 0 not null,
   TJZBGS_BL            NUMERIC               default 0 not null,
   TJZBGS_JSFS          NVARCHAR(4),
   TJZBGS_GSLX          NVARCHAR(40),
   TJZBGS_JSGS          NVARCHAR(4000),
   TJZBGS_MIN           NUMERIC               default 0 not null,
   TJZBGS_MAX           NUMERIC               default 0 not null,
   TJZBGS_JSSX          NUMERIC               default 0 not null,
   TJZBGS_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_TJZBGS primary key (TJZBGS_ZBLX, TJZBGS_ZBBM)
);

/*==============================================================*/
/* Table: TJZBGSBF                                              */
/*==============================================================*/
create table TJZBGSBF 
(
   TJZBGS_ZBLX          NVARCHAR(1)          not null,
   TJZBGS_ZBBM          NUMERIC               not null,
   TJZBGS_ZBMC          NVARCHAR(40),
   TJZBGS_ZBFZID        NVARCHAR(4),
   TJZBGS_JLDW          NVARCHAR(40),
   TJZBGS_XSWS          NUMERIC               not null,
   TJZBGS_BL            NUMERIC               not null,
   TJZBGS_JSFS          NVARCHAR(4),
   TJZBGS_GSLX          NVARCHAR(40),
   TJZBGS_JSGS          NVARCHAR(4000),
   TJZBGS_MIN           NUMERIC               not null,
   TJZBGS_MAX           NUMERIC               not null,
   TJZBGS_JSSX          NUMERIC               not null,
   TJZBGS_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE
);

/*==============================================================*/
/* Table: TJZZB                                                 */
/*==============================================================*/
create table TJZZB 
(
   TJZZB_RQ             DATE                 not null,
   TJZZB_BCID           NVARCHAR(1)          not null,
   TJZZB_ZBFZID         NVARCHAR(4)          not null,
   TJZZB_BZID           NVARCHAR(4),
   TJZZB_ZBID           NVARCHAR(1),
   TJZZB_QR             NVARCHAR(1),
   TJZZB_QRRID          NVARCHAR(10),
   TJZZB_QRR            NVARCHAR(30),
   TJZZB_QRSJ           DATE,
   TJZZB_BZ             NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_TJZZB primary key (TJZZB_RQ, TJZZB_BCID, TJZZB_ZBFZID)
);

/*==============================================================*/
/* Table: TJZZBZ                                                */
/*==============================================================*/
create table TJZZBZ 
(
   TJZZBZ_RQ            DATE                 not null,
   TJZZBZ_BCID          NVARCHAR(1)          not null,
   TJZZBZ_ZBFZID        NVARCHAR(4)          not null,
   TJZZBZ_ZBBM          NUMERIC               default 0 not null,
   TJZZBZ_SQZ           NUMERIC               default 0 not null,
   TJZZBZ_ZBZ           NUMERIC               default 0 not null,
   TJZZBZ_BZID          NVARCHAR(4),
   TJZZBZ_ZBID          NVARCHAR(1),
   TJZZBZ_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_TJZZBZ primary key (TJZZBZ_RQ, TJZZBZ_BCID, TJZZBZ_ZBFZID, TJZZBZ_ZBBM)
);

/*==============================================================*/
/* Table: UCACHE                                                */
/*==============================================================*/
create table UCACHE 
(
   UCACHE_TABLENAME     NVARCHAR(20)         not null,
   UCACHE_UPDTIME       DATE,
   constraint PK_UCACHE primary key (UCACHE_TABLENAME)
);

/*==============================================================*/
/* Table: UGUSER                                                */
/*==============================================================*/
create table UGUSER 
(
   UGUSER_UGID          NVARCHAR(10)         not null,
   UGUSER_USERID        NVARCHAR(10)         not null,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_UGUSER primary key (UGUSER_UGID, UGUSER_USERID)
);

/*==============================================================*/
/* Table: USERGROUP                                             */
/*==============================================================*/
create table USERGROUP 
(
   USERGROUP_UGID       NVARCHAR(10)         not null,
   USERGROUP_UGNAME     NVARCHAR(30),
   USERGROUP_DISABLED   NVARCHAR(1)          default N'N',
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_USERGROUP primary key (USERGROUP_UGID)
);

/*==============================================================*/
/* Table: USERS                                                 */
/*==============================================================*/
create table USERS 
(
   USERS_USERID         NVARCHAR(10)         not null,
   USERS_USERNAME       NVARCHAR(30),
   USERS_PASSWORD       NVARCHAR(32),
   USERS_ABBR           NVARCHAR(10),
   USERS_TYPE           NVARCHAR(1),
   USERS_TEL            NVARCHAR(20),
   USERS_MOBILE         NVARCHAR(20),
   USERS_EMAIL          NVARCHAR(30),
   USERS_DISABLED       NVARCHAR(1),
   USERS_PASSWORDSETTIME DATE,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   USERS_DCRY           NVARCHAR(1),
   USERS_BMID           NVARCHAR(10),
   USERS_BZBH           NVARCHAR(4),
   USERS_BZID           NVARCHAR(4),
   USERS_ZBID           NVARCHAR(1),
   USERS_MAC            NVARCHAR(17),
   USERS_GWID           NVARCHAR(4),
   constraint PK_USERS primary key (USERS_USERID)
);

/*==============================================================*/
/* Table: UTOID                                                 */
/*==============================================================*/
create table UTOID 
(
   UTOID_NAME           NVARCHAR(10)         not null,
   UTOID_VALUE          NUMERIC               default 0 not null,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_UTOID primary key (UTOID_NAME)
);

/*==============================================================*/
/* Table: U_TJRZBZ                                              */
/*==============================================================*/
create table U_TJRZBZ 
(
   TJRZBZ_RQ            DATE                 not null,
   TJRZBZ_ZBFZID        NVARCHAR(4)          not null,
   TJRZBZ_ZBBM          NUMERIC               not null,
   TJRZBZ_SQZ           NUMERIC               not null,
   TJRZBZ_ZBZ           NUMERIC               not null,
   TJRZBZ_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE
);

/*==============================================================*/
/* Table: U_ZBSX                                                */
/*==============================================================*/
create table U_ZBSX 
(
   ZBBM                 NVARCHAR(20),
   ZBMC                 NVARCHAR(100),
   XSSX                 FLOAT,
   ND                   FLOAT,
   YF                   FLOAT
);

/*==============================================================*/
/* Table: U_ZCFZ                                                */
/*==============================================================*/
create table U_ZCFZ 
(
   ZCBM                 NUMERIC,
   ZCZ                  NUMERIC,
   FZBM                 NUMERIC,
   FZZ                  NUMERIC,
   ZCMC                 NVARCHAR(200),
   FZMC                 NVARCHAR(200),
   ND                   FLOAT,
   YF                   FLOAT,
   ZBFZ                 NVARCHAR(20),
   XSSX                 FLOAT,
   ZCNC                 FLOAT,
   FZNC                 FLOAT
);

/*==============================================================*/
/* Table: V_MODULE                                              */
/*==============================================================*/
create table V_MODULE 
(
   V_MODULE_ID          NUMERIC               not null,
   V_MODULE_MDID        NUMERIC               not null,
   V_MODULE_XZ          NVARCHAR(1),
   V_MODULE_VMTIME      DATE,
   constraint PK_V_MODULE primary key (V_MODULE_ID, V_MODULE_MDID)
);

/*==============================================================*/
/* Table: WFB                                                   */
/*==============================================================*/
create table WFB 
(
   WFB_WFBID            NVARCHAR(10)         not null,
   WFB_WFBNAME          NVARCHAR(20),
   WFB_WFMORE           NVARCHAR(1),
   WFB_WFTABLE          NVARCHAR(20),
   WFB_WFIDFIELD        NVARCHAR(30),
   WFB_WFRUNIDFIELD     NVARCHAR(30),
   WFB_WFPIDFIELD       NVARCHAR(30),
   WFB_VETO             NVARCHAR(1),
   WFB_DISPATCH         NVARCHAR(1),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_WFB primary key (WFB_WFBID)
);

/*==============================================================*/
/* Table: WFD                                                   */
/*==============================================================*/
create table WFD 
(
   WFD_WFID             NVARCHAR(10)         not null,
   WFD_WFNAME           NVARCHAR(30),
   WFD_WFBID            NVARCHAR(10),
   WFD_DESC             NVARCHAR(40),
   WFD_SUBJECT          NVARCHAR(250),
   WFD_EXECON           NVARCHAR(250),
   WFD_MANAGER          NVARCHAR(50),
   WFD_DISPATCHER       NVARCHAR(50),
   WFD_BEGINDATE        DATE,
   WFD_ENDDATE          DATE,
   WFD_VECTOR           BINARY,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_WFD primary key (WFD_WFID)
);

/*==============================================================*/
/* Table: WFFR                                                  */
/*==============================================================*/
create table WFFR 
(
   WFFR_WFID            NVARCHAR(10)         not null,
   WFFR_WFPID           NVARCHAR(4)          not null,
   WFFR_CONTROLID       NVARCHAR(40)         not null,
   WFFR_RIGHT           NVARCHAR(1),
   WFFR_CTRLNULL        NVARCHAR(1),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_WFFR primary key (WFFR_WFID, WFFR_WFPID, WFFR_CONTROLID)
);

/*==============================================================*/
/* Table: WFFS                                                  */
/*==============================================================*/
create table WFFS 
(
   WFFS_WFID            NVARCHAR(10)         not null,
   WFFS_WFPID           NVARCHAR(4)          not null,
   WFFS_NO              NUMERIC               default 0 not null,
   WFFS_FIELD           NVARCHAR(30),
   WFFS_FIELDTYPE       NVARCHAR(1),
   WFFS_SYNCMODE        NVARCHAR(1),
   WFFS_SYNCACTION      NVARCHAR(1),
   WFFS_SYNCCONTENT     NVARCHAR(250),
   WFFS_SYNCSQL         NVARCHAR(4000),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_WFFS primary key (WFFS_WFID, WFFS_WFPID, WFFS_NO)
);

/*==============================================================*/
/* Table: WFL                                                   */
/*==============================================================*/
create table WFL 
(
   WFL_WFID             NVARCHAR(10)         not null,
   WFL_WFPID            NVARCHAR(4)          not null,
   WFL_NEXTWFID         NVARCHAR(10)         not null,
   WFL_NEXTWFPID        NVARCHAR(4)          not null,
   WFL_EXECON           NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   WFL_NO               NUMERIC               default 0,
   constraint PK_WFL primary key (WFL_WFID, WFL_WFPID, WFL_NEXTWFID, WFL_NEXTWFPID)
);

/*==============================================================*/
/* Table: WFP                                                   */
/*==============================================================*/
create table WFP 
(
   WFP_WFID             NVARCHAR(10)         not null,
   WFP_WFPID            NVARCHAR(4)          not null,
   WFP_WFPNAME          NVARCHAR(20),
   WFP_TYPE             NVARCHAR(1),
   WFP_DESC             NVARCHAR(40),
   WFP_USERS            NUMERIC               default 0 not null,
   WFP_HOURS            NVARCHAR(30),
   WFP_AMID             NUMERIC               default 0 not null,
   WFP_SETTING          NVARCHAR(30),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   WFP_EXSETTING        NVARCHAR(4000),
   constraint PK_WFP primary key (WFP_WFID, WFP_WFPID)
);

/*==============================================================*/
/* Table: WFPM                                                  */
/*==============================================================*/
create table WFPM 
(
   WFPM_WFBID           NVARCHAR(10)         not null,
   WFPM_WFPMID          NVARCHAR(4)          not null,
   WFPM_WFPMNAME        NVARCHAR(20),
   WFPM_TYPE            NVARCHAR(1),
   WFPM_BELONGTO        NVARCHAR(1),
   WFPM_DEFINE          NVARCHAR(4000),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_WFPM primary key (WFPM_WFBID, WFPM_WFPMID)
);

/*==============================================================*/
/* Table: WFRUN                                                 */
/*==============================================================*/
create table WFRUN 
(
   WFRUN_WFID           NVARCHAR(10)         not null,
   WFRUN_RUNID          NUMERIC               default 0 not null,
   WFRUN_WFPID          NVARCHAR(4),
   WFRUN_STATE          NVARCHAR(1),
   WFRUN_WFPACTION      NVARCHAR(1),
   WFRUN_TITLE          NVARCHAR(250),
   WFRUN_RBEGIN         DATE,
   WFRUN_REND           DATE,
   WFRUN_ABEGIN         DATE,
   WFRUN_TODO           NVARCHAR(250),
   WFRUN_DONE           NVARCHAR(250),
   WFRUN_AGENT          NVARCHAR(250),
   WFRUN_PRIORUSERID    NVARCHAR(10),
   WFRUN_AMID           NUMERIC               default 0 not null,
   WFRUN_KSSJ           DATE,
   WFRUN_JSSJ           DATE,
   WFRUN_PRIORWFPUSERS  NVARCHAR(250),
   WFRUN_REMARKS        NVARCHAR(150),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_WFRUN primary key (WFRUN_WFID, WFRUN_RUNID)
);

/*==============================================================*/
/* Table: WFRUNLOG                                              */
/*==============================================================*/
create table WFRUNLOG 
(
   WFRUNLOG_WFID        NVARCHAR(10)         not null,
   WFRUNLOG_RUNID       NUMERIC               default 0 not null,
   WFRUNLOG_LOGNO       NUMERIC               default 0 not null,
   WFRUNLOG_WFPID       NVARCHAR(4),
   WFRUNLOG_WFPACTION   NVARCHAR(1),
   WFRUNLOG_RBEGIN      DATE,
   WFRUNLOG_REND        DATE,
   WFRUNLOG_ABEGIN      DATE,
   WFRUNLOG_AEND        DATE,
   WFRUNLOG_AUSERID     NVARCHAR(10),
   WFRUNLOG_OPINION     NVARCHAR(250),
   WFRUNLOG_TODO        NVARCHAR(250),
   WFRUNLOG_AGENT       NVARCHAR(250),
   WFRUNLOG_WDNO        NUMERIC               default 0 not null,
   WFRUNLOG_HISTORY     BINARY,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_WFRUNLOG primary key (WFRUNLOG_WFID, WFRUNLOG_RUNID, WFRUNLOG_LOGNO)
);

/*==============================================================*/
/* Table: WFTEST                                                */
/*==============================================================*/
create table WFTEST 
(
   WFTEST_ID            NVARCHAR(10)         not null,
   WFTEST_NAME          NVARCHAR(30),
   WFTEST_NUMERIC       NUMERIC               default 0 not null,
   WFTEST_DESC          NVARCHAR(100),
   WFTEST_WFID          NVARCHAR(10),
   WFTEST_WFRUNID       NUMERIC               default 0 not null,
   WFTEST_WFPID         NVARCHAR(4),
   WFTEST_WFTODO        NVARCHAR(250),
   WFTEST_WFDONE        NVARCHAR(250),
   WFTEST_WFPACTDESC    NVARCHAR(10),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   WFTEST_FGID          NUMERIC               default 0 not null,
   WFTEST_FILES         NUMERIC               default 0 not null,
   constraint PK_WFTEST primary key (WFTEST_ID)
);

/*==============================================================*/
/* Table: WFU                                                   */
/*==============================================================*/
create table WFU 
(
   WFU_WFID             NVARCHAR(10)         not null,
   WFU_WFPID            NVARCHAR(4)          not null,
   WFU_NO               NUMERIC               default 0 not null,
   WFU_TYPE             NVARCHAR(1),
   WFU_CODE             NVARCHAR(32),
   WFU_NAME             NVARCHAR(32),
   WFU_SELECTMODE       NVARCHAR(1),
   WFU_USERS            NUMERIC               default 0 not null,
   WFU_EXECON           NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_WFU primary key (WFU_WFID, WFU_WFPID, WFU_NO)
);

/*==============================================================*/
/* Table: WJGML                                                 */
/*==============================================================*/
create table WJGML 
(
   WJGML_MLID           NVARCHAR(20)         not null,
   WJGML_FMLID          NVARCHAR(20),
   WJGML_MLNAME         NVARCHAR(120),
   WJGML_FGID           NUMERIC,
   WJGML_FILES          NUMERIC,
   WJGML_BZ             NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_WJGML primary key (WJGML_MLID)
);

/*==============================================================*/
/* Table: XXLB                                                  */
/*==============================================================*/
create table XXLB 
(
   XXLB_XXLBID          NVARCHAR(4)          not null,
   XXLB_XXLBMC          NVARCHAR(30),
   XXLB_JSRYGH          NVARCHAR(4000),
   XXLB_JSRYMC          NVARCHAR(4000),
   XXLB_JSBMID          NVARCHAR(4000),
   XXLB_JSBMMC          NVARCHAR(4000),
   XXLB_JSUGID          NVARCHAR(4000),
   XXLB_JSUGMC          NVARCHAR(4000),
   XXLB_XXBLTS          NUMERIC               default 0 not null,
   XXLB_DISABLED        NVARCHAR(1),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_XXLB primary key (XXLB_XXLBID)
);

/*==============================================================*/
/* Table: XXTZ                                                  */
/*==============================================================*/
create table XXTZ 
(
   XXTZ_XXBH            NVARCHAR(14)         not null,
   XXTZ_XXLBID          NVARCHAR(4),
   XXTZ_XXBT            NVARCHAR(100),
   XXTZ_XXNR            NVARCHAR(4000),
   XXTZ_FBRYGH          NVARCHAR(10),
   XXTZ_FBBMID          NVARCHAR(10),
   XXTZ_FBRQ            DATE,
   XXTZ_BLTS            NUMERIC               default 0 not null,
   XXTZ_SJLY            NVARCHAR(1),
   XXTZ_JSRYGH          NVARCHAR(4000),
   XXTZ_JSRYMC          NVARCHAR(4000),
   XXTZ_JSBMID          NVARCHAR(4000),
   XXTZ_JSBMMC          NVARCHAR(4000),
   XXTZ_JSUGID          NVARCHAR(4000),
   XXTZ_JSUGMC          NVARCHAR(4000),
   XXTZ_JSROLEID        NVARCHAR(4000),
   XXTZ_JSROLEMC        NVARCHAR(4000),
   XXTZ_FGID            NUMERIC               default 0 not null,
   XXTZ_FILES           NUMERIC               default 0 not null,
   XXTZ_WFID            NVARCHAR(10),
   XXTZ_WFRUNID         NUMERIC               default 0 not null,
   XXTZ_WFPID           NVARCHAR(4),
   XXTZ_WFTODO          NVARCHAR(250),
   XXTZ_WFDONE          NVARCHAR(250),
   XXTZ_WFPACTDESC      NVARCHAR(10),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_XXTZ primary key (XXTZ_XXBH)
);

/*==============================================================*/
/* Table: YXAC                                                  */
/*==============================================================*/
create table YXAC 
(
   YXAC_ACID            NVARCHAR(6)          not null,
   YXAC_ACLBID          NVARCHAR(4),
   YXAC_DESC            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXAC primary key (YXAC_ACID)
);

/*==============================================================*/
/* Table: YXACA                                                 */
/*==============================================================*/
create table YXACA 
(
   YXACA_GZPXH          NUMERIC               default 0 not null,
   YXACA_XH             NUMERIC               default 0 not null,
   YXACA_XSXH           NUMERIC               default 0 not null,
   YXACA_CSA            NVARCHAR(250),
   YXACA_CSB            NVARCHAR(250),
   YXACA_GXA            NVARCHAR(1),
   YXACA_GXB            NVARCHAR(1),
   YXACA_ZXA            NVARCHAR(250),
   YXACA_ZXB            NVARCHAR(250),
   YXACA_ZXSJA          DATE,
   YXACA_ZXSJB          DATE,
   YXACA_FSA            NVARCHAR(250),
   YXACA_FSB            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXACA primary key (YXACA_GZPXH, YXACA_XH)
);

/*==============================================================*/
/* Table: YXACB                                                 */
/*==============================================================*/
create table YXACB 
(
   YXACB_GZPXH          NUMERIC               default 0 not null,
   YXACB_XH             NUMERIC               default 0 not null,
   YXACB_XSXH           NUMERIC               default 0 not null,
   YXACB_CSA            NVARCHAR(250),
   YXACB_CSB            NVARCHAR(250),
   YXACB_GXA            NVARCHAR(1),
   YXACB_GXB            NVARCHAR(1),
   YXACB_ZXA            NVARCHAR(250),
   YXACB_ZXB            NVARCHAR(250),
   YXACB_ZXSJA          DATE,
   YXACB_ZXSJB          DATE,
   YXACB_FSA            NVARCHAR(250),
   YXACB_FSB            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXACB primary key (YXACB_GZPXH, YXACB_XH)
);

/*==============================================================*/
/* Table: YXACC                                                 */
/*==============================================================*/
create table YXACC 
(
   YXACC_GZPXH          NUMERIC               default 0 not null,
   YXACC_XH             NUMERIC               default 0 not null,
   YXACC_XSXH           NUMERIC               default 0 not null,
   YXACC_CSA            NVARCHAR(250),
   YXACC_CSB            NVARCHAR(250),
   YXACC_GXA            NVARCHAR(1),
   YXACC_GXB            NVARCHAR(1),
   YXACC_ZXA            NVARCHAR(250),
   YXACC_ZXB            NVARCHAR(250),
   YXACC_ZXSJA          DATE,
   YXACC_ZXSJB          DATE,
   YXACC_FSA            NVARCHAR(250),
   YXACC_FSB            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXACC primary key (YXACC_GZPXH, YXACC_XH)
);

/*==============================================================*/
/* Table: YXACD                                                 */
/*==============================================================*/
create table YXACD 
(
   YXACD_GZPXH          NUMERIC               default 0 not null,
   YXACD_XH             NUMERIC               default 0 not null,
   YXACD_XSXH           NUMERIC               default 0 not null,
   YXACD_CSA            NVARCHAR(250),
   YXACD_CSB            NVARCHAR(250),
   YXACD_GXA            NVARCHAR(1),
   YXACD_GXB            NVARCHAR(1),
   YXACD_ZXA            NVARCHAR(250),
   YXACD_ZXB            NVARCHAR(250),
   YXACD_ZXSJA          DATE,
   YXACD_ZXSJB          DATE,
   YXACD_FSA            NVARCHAR(250),
   YXACD_FSB            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXACD primary key (YXACD_GZPXH, YXACD_XH)
);

/*==============================================================*/
/* Table: YXACE                                                 */
/*==============================================================*/
create table YXACE 
(
   YXACE_GZPXH          NUMERIC               default 0 not null,
   YXACE_XH             NUMERIC               default 0 not null,
   YXACE_XSXH           NUMERIC               default 0 not null,
   YXACE_CSA            NVARCHAR(250),
   YXACE_CSB            NVARCHAR(250),
   YXACE_GXA            NVARCHAR(1),
   YXACE_GXB            NVARCHAR(1),
   YXACE_ZXA            NVARCHAR(250),
   YXACE_ZXB            NVARCHAR(250),
   YXACE_ZXSJA          DATE,
   YXACE_ZXSJB          DATE,
   YXACE_FSA            NVARCHAR(250),
   YXACE_FSB            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXACE primary key (YXACE_GZPXH, YXACE_XH)
);

/*==============================================================*/
/* Table: YXACF                                                 */
/*==============================================================*/
create table YXACF 
(
   YXACF_GZPXH          NUMERIC               default 0 not null,
   YXACF_XH             NUMERIC               default 0 not null,
   YXACF_XSXH           NUMERIC               default 0 not null,
   YXACF_CSA            NVARCHAR(250),
   YXACF_CSB            NVARCHAR(250),
   YXACF_GXA            NVARCHAR(1),
   YXACF_GXB            NVARCHAR(1),
   YXACF_ZXA            NVARCHAR(250),
   YXACF_ZXB            NVARCHAR(250),
   YXACF_ZXSJA          DATE,
   YXACF_ZXSJB          DATE,
   YXACF_FSA            NVARCHAR(250),
   YXACF_FSB            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXACF primary key (YXACF_GZPXH, YXACF_XH)
);

/*==============================================================*/
/* Table: YXACG                                                 */
/*==============================================================*/
create table YXACG 
(
   YXACG_GZPXH          NUMERIC               default 0 not null,
   YXACG_XH             NUMERIC               default 0 not null,
   YXACG_XSXH           NUMERIC               default 0 not null,
   YXACG_CSA            NVARCHAR(250),
   YXACG_CSB            NVARCHAR(250),
   YXACG_GXA            NVARCHAR(1),
   YXACG_GXB            NVARCHAR(1),
   YXACG_ZXA            NVARCHAR(250),
   YXACG_ZXB            NVARCHAR(250),
   YXACG_ZXSJA          DATE,
   YXACG_ZXSJB          DATE,
   YXACG_FSA            NVARCHAR(250),
   YXACG_FSB            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXACG primary key (YXACG_GZPXH, YXACG_XH)
);

/*==============================================================*/
/* Table: YXACLB                                                */
/*==============================================================*/
create table YXACLB 
(
   YXACLB_ACLBID        NVARCHAR(4)          not null,
   YXACLB_ACLBMC        NVARCHAR(30),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXACLB primary key (YXACLB_ACLBID)
);

/*==============================================================*/
/* Table: YXBC                                                  */
/*==============================================================*/
create table YXBC 
(
   YXBC_BZID            NVARCHAR(4)          not null,
   YXBC_BCID            NVARCHAR(1)          not null,
   YXBC_BCMC            NVARCHAR(20),
   YXBC_KSSJ            NVARCHAR(5),
   YXBC_KSSJV           NUMERIC               default 0 not null,
   YXBC_KSSJRQ          NVARCHAR(5)          default N'0' not null,
   YXBC_JSSJ            NVARCHAR(5),
   YXBC_JSSJV           NUMERIC               default 0 not null,
   YXBC_JSSJRQ          NVARCHAR(5)          default N'0' not null,
   YXBC_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXBC primary key (YXBC_BZID, YXBC_BCID)
);

/*==============================================================*/
/* Table: YXBZ                                                  */
/*==============================================================*/
create table YXBZ 
(
   YXBZ_BZID            NVARCHAR(4)          not null,
   YXBZ_BZMC            NVARCHAR(40),
   YXBZ_BCS             NUMERIC               default 0 not null,
   YXBZ_ZBS             NUMERIC               default 0 not null,
   YXBZ_ZZBZ            NVARCHAR(1),
   YXBZ_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXBZ primary key (YXBZ_BZID)
);

/*==============================================================*/
/* Table: YXCZP                                                 */
/*==============================================================*/
create table YXCZP 
(
   YXCZP_CZPXH          NUMERIC               default 0 not null,
   YXCZP_CZPBH          NVARCHAR(20),
   YXCZP_PZID           NVARCHAR(10),
   YXCZP_JZID           NVARCHAR(4),
   YXCZP_DXPID          NVARCHAR(10),
   YXCZP_CZRW           NVARCHAR(200),
   YXCZP_KPSJ           DATE,
   YXCZP_KSSJ           DATE,
   YXCZP_JSSJ           DATE,
   YXCZP_FLR            NVARCHAR(30),
   YXCZP_FLSJ           DATE,
   YXCZP_SLR            NVARCHAR(30),
   YXCZP_CZR            NVARCHAR(30),
   YXCZP_JHR            NVARCHAR(30),
   YXCZP_ZBFZR          NVARCHAR(30),
   YXCZP_ZZ             NVARCHAR(30),
   YXCZP_BMID           NVARCHAR(10),
   YXCZP_BZBH           NVARCHAR(4),
   YXCZP_BZ             NVARCHAR(250),
   YXCZP_LCXX           NVARCHAR(4000),
   YXCZP_WFID           NVARCHAR(10),
   YXCZP_WFRUNID        NUMERIC               default 0 not null,
   YXCZP_WFPID          NVARCHAR(4),
   YXCZP_WFTODO         NVARCHAR(1000),
   YXCZP_WFDONE         NVARCHAR(250),
   YXCZP_WFPACTDESC     NVARCHAR(20),
   YXCZP_CZLX           NVARCHAR(40),
   YXCZP_BZID           NVARCHAR(4),
   YXCZP_ZBID           NVARCHAR(1),
   YXCZP_ZYID           NVARCHAR(4),
   YXCZP_SHR            NVARCHAR(30),
   YXCZP_SHSJ           DATE,
   YXCZP_SHJG           NVARCHAR(10),
   YXCZP_DDZLH          NVARCHAR(20),
   YXCZP_YXZT           NVARCHAR(100),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXCZP primary key (YXCZP_CZPXH)
);

/*==============================================================*/
/* Table: YXCZPXM                                               */
/*==============================================================*/
create table YXCZPXM 
(
   YXCZPXM_CZPXH        NUMERIC               default 0 not null,
   YXCZPXM_XH           NUMERIC               default 0 not null,
   YXCZPXM_XSXH         NUMERIC               default 0 not null,
   YXCZPXM_CZXM         NVARCHAR(250),
   YXCZPXM_YCZ          NVARCHAR(1),
   YXCZPXM_WCSJ         DATE,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   YXCZPXM_ZYSX         NVARCHAR(250),
   constraint PK_YXCZPXM primary key (YXCZPXM_CZPXH, YXCZPXM_XH)
);

/*==============================================================*/
/* Table: YXGW                                                  */
/*==============================================================*/
create table YXGW 
(
   YXGW_GWID            NVARCHAR(4)          not null,
   YXGW_GWMC            NVARCHAR(40),
   YXGW_BZID            NVARCHAR(4),
   YXGW_JSZTLIST        NVARCHAR(250),
   YXGW_SBZTLIST        NVARCHAR(250),
   YXGW_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   YXGW_YRJSGW          NVARCHAR(250),
   constraint PK_YXGW primary key (YXGW_GWID)
);

/*==============================================================*/
/* Table: YXGWCY                                                */
/*==============================================================*/
create table YXGWCY 
(
   YXGWCY_GWID          NVARCHAR(4)          not null,
   YXGWCY_USERID        NVARCHAR(10)         not null,
   YXGWCY_BZID          NVARCHAR(4),
   YXGWCY_ZBID          NVARCHAR(1),
   YXGWCY_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXGWCY primary key (YXGWCY_GWID, YXGWCY_USERID)
);

/*==============================================================*/
/* Table: YXGWRZ                                                */
/*==============================================================*/
create table YXGWRZ 
(
   YXGWRZ_GWID          NVARCHAR(4)          not null,
   YXGWRZ_RQ            DATE                 not null,
   YXGWRZ_BCID          NVARCHAR(1)          not null,
   YXGWRZ_BZID          NVARCHAR(4),
   YXGWRZ_ZBID          NVARCHAR(1),
   YXGWRZ_DBTQ          NVARCHAR(60),
   YXGWRZ_DBCY          NVARCHAR(250),
   YXGWRZ_DBJBRID       NVARCHAR(10),
   YXGWRZ_DBJBR         NVARCHAR(30),
   YXGWRZ_DBJBSJ        DATE,
   YXGWRZ_JBJBRID       NVARCHAR(10),
   YXGWRZ_JBJBR         NVARCHAR(30),
   YXGWRZ_JBJBSJ        DATE,
   YXGWRZ_RZBB          BINARY,
   YXGWRZ_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXGWRZ primary key (YXGWRZ_GWID, YXGWRZ_RQ, YXGWRZ_BCID)
);

/*==============================================================*/
/* Table: YXGWRZMX                                              */
/*==============================================================*/
create table YXGWRZMX 
(
   YXGWRZMX_GWID        NVARCHAR(4)          not null,
   YXGWRZMX_RQ          DATE                 not null,
   YXGWRZMX_BCID        NVARCHAR(1)          not null,
   YXGWRZMX_XH          NUMERIC               default 0 not null,
   YXGWRZMX_RZZT        NVARCHAR(60),
   YXGWRZMX_SJ          NVARCHAR(5),
   YXGWRZMX_RQSJ        DATE,
   YXGWRZMX_RZNR        NVARCHAR(4000),
   YXGWRZMX_BZ          NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXGWRZMX primary key (YXGWRZMX_GWID, YXGWRZMX_RQ, YXGWRZMX_BCID, YXGWRZMX_XH)
);

/*==============================================================*/
/* Table: YXGWRZSB                                              */
/*==============================================================*/
create table YXGWRZSB 
(
   YXGWRZSB_GWID        NVARCHAR(4)          not null,
   YXGWRZSB_RQ          DATE                 not null,
   YXGWRZSB_BCID        NVARCHAR(1)          not null,
   YXGWRZSB_SBXH        NUMERIC               default 0 not null,
   YXGWRZSB_SQZT        NVARCHAR(100),
   YXGWRZSB_BQZT        NVARCHAR(100),
   YXGWRZSB_ZT0         NVARCHAR(1),
   YXGWRZSB_ZT1         NVARCHAR(1),
   YXGWRZSB_ZT2         NVARCHAR(1),
   YXGWRZSB_ZT3         NVARCHAR(1),
   YXGWRZSB_ZT4         NVARCHAR(1),
   YXGWRZSB_ZT5         NVARCHAR(1),
   YXGWRZSB_ZT6         NVARCHAR(1),
   YXGWRZSB_ZT7         NVARCHAR(1),
   YXGWRZSB_ZT8         NVARCHAR(1),
   YXGWRZSB_ZT9         NVARCHAR(1),
   YXGWRZSB_ZTSM        NVARCHAR(250),
   YXGWRZSB_BZ          NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXGWRZSB primary key (YXGWRZSB_GWID, YXGWRZSB_RQ, YXGWRZSB_BCID, YXGWRZSB_SBXH)
);

/*==============================================================*/
/* Table: YXGWSB                                                */
/*==============================================================*/
create table YXGWSB 
(
   YXGWSB_GWID          NVARCHAR(4)          not null,
   YXGWSB_SBXH          NUMERIC               default 0 not null,
   YXGWSB_SBMC          NVARCHAR(40),
   YXGWSB_XSSX          NUMERIC               default 0 not null,
   YXGWSB_BZ            NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXGWSB primary key (YXGWSB_GWID, YXGWSB_SBXH)
);

/*==============================================================*/
/* Table: YXGZP                                                 */
/*==============================================================*/
create table YXGZP 
(
   YXGZP_GZPXH          NUMERIC               default 0 not null,
   YXGZP_FGZPXH         NUMERIC               default 0 not null,
   YXGZP_GZPBH          NVARCHAR(20),
   YXGZP_FGZPBH         NVARCHAR(20),
   YXGZP_PZID           NVARCHAR(10),
   YXGZP_KPR            NVARCHAR(30),
   YXGZP_KPSJ           DATE,
   YXGZP_GDBH           NVARCHAR(20),
   YXGZP_GZBMID         NVARCHAR(10),
   YXGZP_GZBZBH         NVARCHAR(4),
   YXGZP_GZRY           NVARCHAR(250),
   YXGZP_GZRS           NUMERIC               default 0 not null,
   YXGZP_GZFZR          NVARCHAR(30),
   YXGZP_GZDD           NVARCHAR(100),
   YXGZP_GZNR           NVARCHAR(250),
   YXGZP_GZJHKSSJ       DATE,
   YXGZP_GZJHJSSJ       DATE,
   YXGZP_ZZ             NVARCHAR(30),
   YXGZP_QFR            NVARCHAR(30),
   YXGZP_GZLXR          NVARCHAR(30),
   YXGZP_QFSJ           DATE,
   YXGZP_PZKSSJ         DATE,
   YXGZP_PZJSSJ         DATE,
   YXGZP_XKKGSJ         DATE,
   YXGZP_GZXKR          NVARCHAR(30),
   YXGZP_ZJSJ           DATE,
   YXGZP_ZJXKR          NVARCHAR(30),
   YXGZP_SBMC           NVARCHAR(40),
   YXGZP_JZMC           NVARCHAR(40),
   YXGZP_YQJSSJ         DATE,
   YXGZP_BGGZFZR        NVARCHAR(30),
   YXGZP_BGSJ           DATE,
   YXGZP_JDXZS          NUMERIC               default 0 not null,
   YXGZP_JDXBH          NVARCHAR(100),
   YXGZP_JDXCCBH        NVARCHAR(100),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   YXGZP_SBBM           NVARCHAR(40),
   YXGZP_JZID           NVARCHAR(4),
   YXGZP_LCXX           NVARCHAR(4000),
   YXGZP_WFID           NVARCHAR(10),
   YXGZP_WFRUNID        NUMERIC               default 0 not null,
   YXGZP_WFPID          NVARCHAR(4),
   YXGZP_WFTODO         NVARCHAR(250),
   YXGZP_WFDONE         NVARCHAR(250),
   YXGZP_WFPACTDESC     NVARCHAR(20),
   YXGZP_DXP            NVARCHAR(1)          default N'N' not null,
   YXGZP_DXGZPXH        NUMERIC               default 0 not null,
   YXGZP_GZTJTD         NVARCHAR(1),
   YXGZP_JPR            NVARCHAR(30),
   YXGZP_JPSJ           DATE,
   YXGZP_ZBFZR          NVARCHAR(30),
   YXGZP_ZJZBFZR        NVARCHAR(30),
   YXGZP_YQZZ           NVARCHAR(30),
   constraint PK_YXGZP primary key (YXGZP_GZPXH)
);

/*==============================================================*/
/* Table: YXPBB                                                 */
/*==============================================================*/
create table YXPBB 
(
   YXPBB_RQ             DATE                 not null,
   YXPBB_BZID           NVARCHAR(4)          not null,
   YXPBB_BCID           NVARCHAR(1)          not null,
   YXPBB_ZBID           NVARCHAR(1),
   YXPBB_BZ             NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXPBB primary key (YXPBB_RQ, YXPBB_BZID, YXPBB_BCID)
);

/*==============================================================*/
/* Table: YXWXD                                                 */
/*==============================================================*/
create table YXWXD 
(
   YXWXD_WXDID          NVARCHAR(6)          not null,
   YXWXD_WXDLBID        NVARCHAR(4),
   YXWXD_DESC           NVARCHAR(250),
   YXWXD_KZCS           NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXWXD primary key (YXWXD_WXDID)
);

/*==============================================================*/
/* Table: YXWXDLB                                               */
/*==============================================================*/
create table YXWXDLB 
(
   YXWXDLB_WXDLBID      NVARCHAR(4)          not null,
   YXWXDLB_WXDLBMC      NVARCHAR(30),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXWXDLB primary key (YXWXDLB_WXDLBID)
);

/*==============================================================*/
/* Table: YXZB                                                  */
/*==============================================================*/
create table YXZB 
(
   YXZB_BZID            NVARCHAR(4)          not null,
   YXZB_ZBID            NVARCHAR(1)          not null,
   YXZB_ZBMC            NVARCHAR(20),
   YXZB_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_YXZB primary key (YXZB_BZID, YXZB_ZBID)
);

/*==============================================================*/
/* Table: ZBDY                                                  */
/*==============================================================*/
create table ZBDY 
(
   ZBDY_ZBID            NVARCHAR(20)         not null,
   ZBDY_ZBMC            NVARCHAR(80),
   ZBDY_FLID            NVARCHAR(10),
   ZBDY_TXID            NVARCHAR(10),
   ZBDY_SXZQ            FLOAT,
   ZBDY_MDID            FLOAT,
   ZBDY_URL             NVARCHAR(200),
   ZBDY_ZT              NVARCHAR(1),
   ZBDY_BZ              NVARCHAR(250),
   ZBDY_TXSXDY1         NVARCHAR(4000),
   ZBDY_ZXSJBDJB1       NVARCHAR(4000),
   ZBDY_SQL             NVARCHAR(4000),
   ZBDY_SQLSJK          NVARCHAR(60),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   ZBDY_ZDYJS1          NVARCHAR(4000),
   ZBDY_TXSXDY2         NVARCHAR(4000),
   ZBDY_TXSXDY          NTEXT,
   ZBDY_ZXSJBDJB        NTEXT,
   ZBDY_ZDYJS           NTEXT,
   constraint PK_ZBDY primary key (ZBDY_ZBID)
);

/*==============================================================*/
/* Table: ZBFL                                                  */
/*==============================================================*/
create table ZBFL 
(
   ZBFL_FLID            NVARCHAR(10)         not null,
   ZBFL_FLMC            NVARCHAR(60),
   ZBFL_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_ZBFL primary key (ZBFL_FLID)
);

/*==============================================================*/
/* Table: ZBSY                                                  */
/*==============================================================*/
create table ZBSY 
(
   ZBSY_SYID            NVARCHAR(10)         not null,
   ZBSY_MC              NVARCHAR(60),
   ZBSY_COLS            FLOAT,
   ZBSY_ROWS            FLOAT,
   ZBSY_MDID            FLOAT,
   ZBSY_BZ              NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_ZBSY primary key (ZBSY_SYID)
);

/*==============================================================*/
/* Table: ZBSYMX                                                */
/*==============================================================*/
create table ZBSYMX 
(
   ZBSYMX_SYID          NVARCHAR(10)         not null,
   ZBSYMX_ZBID          NVARCHAR(20)         not null,
   ZBSYMX_XSBT          NVARCHAR(30),
   ZBSYMX_ZDXS          NVARCHAR(1),
   ZBSYMX_XSZQ          FLOAT,
   ZBSYMX_COL           FLOAT,
   ZBSYMX_ROW           FLOAT,
   ZBSYMX_BZ            NVARCHAR(250),
   ZBSYMX_HEIGHT        FLOAT,
   ZBSYMX_WIDTH         FLOAT,
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   CREATEDTIME          DATE,
   constraint PK_ZBSYMX primary key (ZBSYMX_SYID, ZBSYMX_ZBID)
);

/*==============================================================*/
/* Table: ZDYCX                                                 */
/*==============================================================*/
create table ZDYCX 
(
   ZDYCX_MDID           NUMERIC               not null,
   ZDYCX_CXMC           NVARCHAR(80),
   ZDYCX_DBNAME         NVARCHAR(10),
   ZDYCX_ZDYSQL         NVARCHAR(4000),
   ZDYCX_BZ             NVARCHAR(250),
   CREATERID            NVARCHAR(10),
   CREATER              NVARCHAR(30),
   CREATEDTIME          DATE,
   MODIFIERID           NVARCHAR(10),
   MODIFIER             NVARCHAR(30),
   MODIFIEDTIME         DATE,
   constraint PK_ZDYCX primary key (ZDYCX_MDID)
);

/*==============================================================*/
/* Table: ZDYCXJG                                               */
/*==============================================================*/
create table ZDYCXJG 
(
   ZDYCXJG_MDID         NUMERIC               not null,
   ZDYCXJG_JGMC         NVARCHAR(40)         not null,
   ZDYCXJG_JGLBT        NVARCHAR(80),
   ZDYCXJG_JGLKD        NUMERIC,
   ZDYCXJG_JGLLX        NVARCHAR(10),
   ZDYCXJG_JGLSX        NUMERIC,
   ZDYCXJG_JGLCSH       NVARCHAR(4000),
   ZDYCXJG_BZ           NVARCHAR(250),
   constraint PK_ZDYCXJG primary key (ZDYCXJG_MDID, ZDYCXJG_JGMC)
);

/*==============================================================*/
/* Table: ZDYCXTJ                                               */
/*==============================================================*/
create table ZDYCXTJ 
(
   ZDYCXTJ_MDID         NUMERIC               not null,
   ZDYCXTJ_TJMC         NVARCHAR(40)         not null,
   ZDYCXTJ_TJCSM        NVARCHAR(40),
   ZDYCXTJ_TJBQ         NVARCHAR(80),
   ZDYCXTJ_TJLX         NVARCHAR(10),
   ZDYCXTJ_MRZ          NVARCHAR(80),
   ZDYCXTJ_CSH          NVARCHAR(4000),
   ZDYCXTJ_SFBT         NVARCHAR(1),
   ZDYCXTJ_HWZ          NUMERIC,
   ZDYCXTJ_LWZ          NUMERIC,
   ZDYCXTJ_BZ           NVARCHAR(250),
   ZDYCXTJ_CXFS         NVARCHAR(10),
   ZDYCXTJ_TJKD         NUMERIC               default 120,
   constraint PK_ZDYCXTJ primary key (ZDYCXTJ_MDID, ZDYCXTJ_TJMC)
);

/*==============================================================*/
/* 初始化控制中心菜单                                           */
/*==============================================================*/


INSERT INTO FUNCMODEL 
(FUNCMODEL_CHILDID, FUNCMODEL_ORDER, FUNCMODEL_FATHERID, FUNCMODEL_NAME, FUNCMODEL_TYPE, CREATERID, CREATER, CREATEDTIME, MODIFIERID, MODIFIER, MODIFIEDTIME) 
VALUES 
(-100000, 10, 0, N'控制中心', N'C', NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO FUNCMODEL 
(FUNCMODEL_CHILDID, FUNCMODEL_ORDER, FUNCMODEL_FATHERID, FUNCMODEL_NAME, FUNCMODEL_TYPE, CREATERID, CREATER, CREATEDTIME, MODIFIERID, MODIFIER, MODIFIEDTIME) 
VALUES 
(102010001, 10, -100000, N'系统信息', N'A', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO FUNCMODEL 
(FUNCMODEL_CHILDID, FUNCMODEL_ORDER, FUNCMODEL_FATHERID, FUNCMODEL_NAME, FUNCMODEL_TYPE, CREATERID, CREATER, CREATEDTIME, MODIFIERID, MODIFIER, MODIFIEDTIME) 
VALUES 
(102010002, 20, -100000, N'用户', N'A', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO FUNCMODEL 
(FUNCMODEL_CHILDID, FUNCMODEL_ORDER, FUNCMODEL_FATHERID, FUNCMODEL_NAME, FUNCMODEL_TYPE, CREATERID, CREATER, CREATEDTIME, MODIFIERID, MODIFIER, MODIFIEDTIME) 
VALUES 
(102010003, 30, -100000, N'角色', N'A', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO FUNCMODEL 
(FUNCMODEL_CHILDID, FUNCMODEL_ORDER, FUNCMODEL_FATHERID, FUNCMODEL_NAME, FUNCMODEL_TYPE, CREATERID, CREATER, CREATEDTIME, MODIFIERID, MODIFIER, MODIFIEDTIME) 
VALUES (102010004, 40, -100000, N'部件', N'A', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO FUNCMODEL 
(FUNCMODEL_CHILDID, FUNCMODEL_ORDER, FUNCMODEL_FATHERID, FUNCMODEL_NAME, FUNCMODEL_TYPE, CREATERID, CREATER, CREATEDTIME, MODIFIERID, MODIFIER, MODIFIEDTIME) 
VALUES 
(102010005, 50, -100000, N'模块', N'A', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO FUNCMODEL 
(FUNCMODEL_CHILDID, FUNCMODEL_ORDER, FUNCMODEL_FATHERID, FUNCMODEL_NAME, FUNCMODEL_TYPE, CREATERID, CREATER, CREATEDTIME, MODIFIERID, MODIFIER, MODIFIEDTIME) 
VALUES 
(102010006, 60, -100000, N'企业模型', N'A', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO FUNCMODEL 
(FUNCMODEL_CHILDID, FUNCMODEL_ORDER, FUNCMODEL_FATHERID, FUNCMODEL_NAME, FUNCMODEL_TYPE, CREATERID, CREATER, CREATEDTIME, MODIFIERID, MODIFIER, MODIFIEDTIME) 
VALUES 
(102010007, 70, -100000, N'部门', N'A', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO FUNCMODEL 
(FUNCMODEL_CHILDID, FUNCMODEL_ORDER, FUNCMODEL_FATHERID, FUNCMODEL_NAME, FUNCMODEL_TYPE, CREATERID, CREATER, CREATEDTIME, MODIFIERID, MODIFIER, MODIFIEDTIME) 
VALUES (102010008, 8, -100000, N'小助手', N'A', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO FUNCMODEL 
(FUNCMODEL_CHILDID, FUNCMODEL_ORDER, FUNCMODEL_FATHERID, FUNCMODEL_NAME, FUNCMODEL_TYPE, CREATERID, CREATER, CREATEDTIME, MODIFIERID, MODIFIER, MODIFIEDTIME) 
VALUES 
(102010009, 80, -100000, N'数据库', N'A', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO FUNCMODEL 
(FUNCMODEL_CHILDID, FUNCMODEL_ORDER, FUNCMODEL_FATHERID, FUNCMODEL_NAME, FUNCMODEL_TYPE, CREATERID, CREATER, CREATEDTIME, MODIFIERID, MODIFIER, MODIFIEDTIME) 
VALUES 
(102010010, 90, -100000, N'参数配置', N'A', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO FUNCMODEL 
(FUNCMODEL_CHILDID, FUNCMODEL_ORDER, FUNCMODEL_FATHERID, FUNCMODEL_NAME, FUNCMODEL_TYPE, CREATERID, CREATER, CREATEDTIME, MODIFIERID, MODIFIER, MODIFIEDTIME) 
VALUES 
(102010011, 100, -100000, N'用户组', N'A', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO FUNCMODEL 
(FUNCMODEL_CHILDID, FUNCMODEL_ORDER, FUNCMODEL_FATHERID, FUNCMODEL_NAME, FUNCMODEL_TYPE, CREATERID, CREATER, CREATEDTIME, MODIFIERID, MODIFIER, MODIFIEDTIME) 
VALUES 
(102011979, 110, -100000, N'服务监测', N'A', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO FUNCMODEL 
(FUNCMODEL_CHILDID, FUNCMODEL_ORDER, FUNCMODEL_FATHERID, FUNCMODEL_NAME, FUNCMODEL_TYPE, CREATERID, CREATER, CREATEDTIME, MODIFIERID, MODIFIER, MODIFIEDTIME) 
VALUES 
(102019997, 120, -100000, N'流程文件测试', N'A', NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO [dbo].[SYSTEM] ([SYSTEM_ID], [SYSTEM_NAME], [SYSTEM_PSWLENGTH], [SYSTEM_PSWDAYS], [SYSTEM_PSWWARNDAYS], [SYSTEM_PSWNEW], [SYSTEM_PSWHISTORYCOUNT], [CREATERID], [CREATER], [CREATEDTIME], [MODIFIERID], [MODIFIER], [MODIFIEDTIME], [SYSTEM_LIMITEDDATE], [SYSTEM_MACHINEID], [SYSTEM_KEY]) VALUES (N'0000000001', N'Application Name', CAST(1 AS Decimal(18, 0)), CAST(1 AS Decimal(18, 0)), CAST(1 AS Decimal(18, 0)), N'1', CAST(1 AS Decimal(18, 0)), N'1', N'1', NULL, N'1', N'1', NULL, N'9999-12-31', N'1', N'1')
update users set users_password = N'9429616146' where users_userid = 'admin'

select * from USERS


--创建登陆帐户（create login）
create login dba with password='dba', default_database=TiMi

--为登陆账户创建数据库用户（create user）,在mydb数据库中的security中的user下可以找到新创建的dba
create user dba for login dba with default_schema=dbo

--通过加入数据库角色，赋予数据库用户“db_owner”权限
exec sp_addrolemember 'db_owner', 'dba'



select * from SYSTEM

exec sp_helpuser 'dba'


CREATE TABLE XMLB
    (
      XMLB_XMLB NVARCHAR(10) NOT NULL
                             PRIMARY KEY ,
      XMLB_MC NVARCHAR(30) NOT NULL ,
      XMLB_QYRQ DATE ,
      XMLB_TYBZ NVARCHAR(30) ,
      XMLB_TYRQ DATE ,
      XMLB_BMGZ NVARCHAR(20) ,
      CREATERID NVARCHAR(10) ,
      CREATER NVARCHAR(30) ,
      CREATEDTIME DATE ,
      MODIFIERID NVARCHAR(10) ,
      MODIFIER NVARCHAR(30) ,
      MODIFIEDTIME DATE
    );


	--create table KHXM
--(
--  KHXM_ID           CHAR(20),
--  KHXM_MC           CHAR(20),
--  KHXM_QYRQ         DATE,
--  KHXM_TYRQ         DATE,
--  KHXM_TYBZ         NVARCHAR(10),
--  KHXM_BZ           NVARCHAR(30),
--  KHXM_CREATERID    NVARCHAR(10),
--  KHXM_CREATER      NVARCHAR(30),
--  KHXM_CREATEDTIME  DATE,
--  KHXM_MODIFIERID   NVARCHAR(10),
--  KHXM_MODIFIER     NVARCHAR(30),
--  KHXM_MODIFIEDTIME DATE
--)
