from asyncio import constants
from venv import logger
from exceptions import SFTPException
from services import FileHandlerService, SFTPFileFetcher, SFTPService 

from typing import List
from interfaces import IFileHandlerService, ISFTPService


class SFTPFileFetcher:
    def __init__(self, sftp_service: ISFTPService, file_handler: IFileHandlerService):
        self.sftp_service = sftp_service
        self.file_handler = file_handler

    def fetch_and_filter_sftp_files(self, directory: str, condition) -> List:
        self.sftp_service.connect()
        try:
            return self.file_handler.fetch_and_filter_files(directory, condition)
        finally:
            self.sftp_service.close()

def fetch_and_filter_sftp_files(secret: str, directory: str, condition) -> List:
    sftp_service = SFTPService(secret)
    file_handler = FileHandlerService(sftp_service)
    file_fetcher = SFTPFileFetcher(sftp_service, file_handler)

    try:
        return file_fetcher.fetch_and_filter_sftp_files(directory, condition)
    except SFTPException as e:
        logger.error(constants.ERROR_FETCHING_FILES)
        raise e

def condition(file) -> bool:
    return True

# Fetch and filter files
secret = constants.DEFAULT_SECRET
directory = constants.DIRECTORY_PATH
files = fetch_and_filter_sftp_files(secret, directory, condition)
