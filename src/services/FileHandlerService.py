from typing import List
from interfaces import IFileHandlerService, ISFTPService


class FileHandlerService(IFileHandlerService):
    def __init__(self, sftp_service: ISFTPService):
        self.sftp_service = sftp_service

    def fetch_and_filter_files(self, directory: str, condition) -> List:
        """Fetch and filter files from the directory using the given condition."""
        files = self.sftp_service.fetch_files(directory)
        return [file for file in files if condition(file)]