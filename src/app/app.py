from asyncio import constants
from venv import logger
from exceptions import SFTPException
from services import FileHandlerService, SFTPFileFetcher, SFTPService 

from typing import List
from interfaces import IFileHandlerService, ISFTPService


def _fetch_and_filter_sftp_files(self, directory: str, condition) -> List:
        sftp_service = SFTPService(secret)
        file_handler = FileHandlerService(sftp_service)

        sftp_service.connect()
        try:
            return file_handler.fetch_and_filter_files(directory, condition)
        except SFTPException as e:
            logger.error(constants.ERROR_FETCHING_FILES)
            raise e
        finally:
            sftp_service.close()

def fetch_and_filter_sftp_files(secret: str, directory: str, condition) -> List:
    return _fetch_and_filter_sftp_files(directory, condition)
    
def condition(file) -> bool:
    return True

# Fetch and filter files
secret = constants.DEFAULT_SECRET
directory = constants.DIRECTORY_PATH
files = fetch_and_filter_sftp_files(secret, directory, condition)
